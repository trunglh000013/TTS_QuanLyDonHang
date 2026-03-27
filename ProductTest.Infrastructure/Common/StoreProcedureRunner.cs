using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductTest.Application.Abstractions;
using ProductTest.Infrastructure.Persistence;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace ProductTest.Infrastructure.Common
{
    /// <summary>
    /// Executes stored procedures and maps results to objects via reflection (columns <-> properties of <typeparamref name="TResult"/>).
    /// This approach does <b>not</b> require <c>DbSet&lt;TResult&gt;</c> in <see cref="ProductDbContext"/> — useful when <typeparamref name="TResult"/> is a DTO.
    /// If you implement an EF Core repository for entities defined in DbContext, use <c>DbSet</c> + LINQ instead of (or alongside) this executor.
    /// </summary>
    public sealed class StoreProcedureRunner : IStoreProcedureRunner
    {
        private readonly ILogger<StoreProcedureRunner> _logger;
        private readonly ProductDbContext _dbContext;

        public StoreProcedureRunner(ProductDbContext dbContext, ILogger<StoreProcedureRunner> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// Retrieves the SQL parameters for the specified stored procedure name from the database.
        /// </summary>
        /// <param name="storeProcedureName">The name of the stored procedure.</param>
        /// <returns>A list of SqlParameter objects that match the stored procedure's parameter definitions.</returns>
        public async Task<List<SqlParameter>> GetSqlParametersFromProcedure(string storeProcedureName)
        {
            if (string.IsNullOrWhiteSpace(storeProcedureName))
                throw new ArgumentException("Stored procedure name cannot be null or empty.", nameof(storeProcedureName));

            var connectionString = _dbContext.Database.GetConnectionString();
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(storeProcedureName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            // Derive parameters from the procedure using SqlCommandBuilder
            SqlCommandBuilder.DeriveParameters(command);

            // The first parameter is often the return value (@RETURN_VALUE); filter it out if needed
            var parameters = command.Parameters
                .Cast<SqlParameter>()
                .Where(p => p.Direction != ParameterDirection.ReturnValue)
                .ToList();

            return parameters;
        }

        /// <summary>
        /// Executes a stored procedure and maps the result to a list of type TResult using Dapper.
        /// </summary>
        public async Task<List<TResult>> ExecuteProcedureAsync<TResult>(
            string storeProcedureName,
            object parameters,
            CancellationToken cancellationToken = default)
            where TResult : class
        {
            if (string.IsNullOrWhiteSpace(storeProcedureName))
                throw new ArgumentException("Stored procedure name cannot be null or empty.", nameof(storeProcedureName));

            var paramProps = parameters?.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance) ?? Array.Empty<PropertyInfo>();
            var paramAssignment = paramProps.Length > 0
                ? string.Join(", ", paramProps.Select(p => $"{p.Name} = {p.GetValue(parameters)}"))
                : "";
            _logger.LogInformation("Executing stored procedure {StoreProcedureName} with params: {Params}",
                storeProcedureName,
                paramAssignment);

            var connectionString = _dbContext.Database.GetConnectionString();
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync(cancellationToken);

            var sqlParameters = await GetSqlParametersFromProcedure(storeProcedureName);

            var dynamicParams = ObjectFlattener.ToDynamicParameters(parameters);

            var mappedParams = new DynamicParameters();

            foreach (var p in sqlParameters)
            {
                var name = p.ParameterName.TrimStart('@');
                var rawValue = dynamicParams.Get<object>(name);
                var convertedValue = SqlParameterValueConverter.Convert(rawValue, p);
                mappedParams.Add("@" + name, convertedValue, dbType: SqlParameterValueConverter.ToDbType(p.SqlDbType));
            }

            var result = (await connection.QueryAsync<TResult>(
                sql: storeProcedureName,
                param: mappedParams,
                commandType: CommandType.StoredProcedure
            )).ToList();

            _logger.LogInformation("Stored procedure {StoreProcedureName} executed successfully. Returned {Count} rows.",
                storeProcedureName,
                result?.Count ?? 0);

            return result ?? [];
        }

        /// <summary>
        /// Executes a stored procedure that does not return entities (for INSERT/UPDATE/DELETE) using Dapper.
        /// </summary>
        public async Task<int> ExecuteNonQueryAsync(
            string storeProcedureName,
            object parameters,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(storeProcedureName))
                throw new ArgumentException("Stored procedure name cannot be null or empty.", nameof(storeProcedureName));

            var paramProps = parameters?.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance) ?? Array.Empty<PropertyInfo>();
            var paramAssignment = paramProps.Length > 0
                ? string.Join(", ", paramProps.Select(p => $"{p.Name} = {p.GetValue(parameters)}"))
                : "";

            _logger.LogInformation("Executing non-query stored procedure {StoreProcedureName} with params: {Params}",
                storeProcedureName,
                paramAssignment);

            var connectionString = _dbContext.Database.GetConnectionString();
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync(cancellationToken);

            var sqlParameters = await GetSqlParametersFromProcedure(storeProcedureName);

            var dynamicParams = ObjectFlattener.ToDynamicParameters(parameters);

            foreach (var paramName in dynamicParams.ParameterNames)
            {
                var paramValue = dynamicParams.Get<object>(paramName);
                _logger.LogInformation("Dynamic parameter: Name = {ParamName}, Value = {ParamValue}", paramName, paramValue);
            }

            var mappedParams = new DynamicParameters();
            MapProcedureParameters(mappedParams, sqlParameters, parameters, dynamicParams);

            var affectedRows = await connection.ExecuteAsync(
                sql: storeProcedureName,
                param: mappedParams,
                commandType: CommandType.StoredProcedure
            );

            _logger.LogInformation("Stored procedure {StoreProcedureName} executed successfully. Affected rows: {AffectedRows}.",
                storeProcedureName,
                affectedRows);

            return affectedRows;
        }

        private static void MapProcedureParameters(
            DynamicParameters mappedParams,
            List<SqlParameter> sqlParameters,
            object? parameters,
            DynamicParameters dynamicParams)
        {
            foreach (var p in sqlParameters)
            {
                var name = p.ParameterName.TrimStart('@');

                if (p.SqlDbType == SqlDbType.Structured)
                {
                    ArgumentNullException.ThrowIfNull(parameters);
                    var table = BuildTvpIdListDataTable(parameters, name);
                    var typeName = p.TypeName?.Trim();
                    if (string.IsNullOrEmpty(typeName))
                        throw new InvalidOperationException($"Stored procedure parameter '{p.ParameterName}' is table-valued but TypeName is missing.");
                    mappedParams.Add("@" + name, table.AsTableValuedParameter(typeName));
                    continue;
                }

                var rawValue = dynamicParams.Get<object>(name);
                var convertedValue = SqlParameterValueConverter.Convert(rawValue, p);
                mappedParams.Add("@" + name, convertedValue, dbType: SqlParameterValueConverter.ToDbType(p.SqlDbType));
            }
        }

        private static DataTable BuildTvpIdListDataTable(object parameters, string parameterNameWithoutAt)
        {
            var prop = parameters.GetType().GetProperty(
                parameterNameWithoutAt,
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (prop?.GetValue(parameters) is not IEnumerable enumerable)
            {
                throw new InvalidOperationException(
                    $"Request type must expose property '{parameterNameWithoutAt}' as an enumerable of strings for TVP @{parameterNameWithoutAt}.");
            }

            var dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(string));
            foreach (var item in enumerable)
            {
                if (item is string s && !string.IsNullOrWhiteSpace(s))
                    dataTable.Rows.Add(s.Trim());
            }

            return dataTable;
        }
    }

    /// <summary>
    /// Helper function to map C# .NET types to SQL Server types (SqlDbType).
    /// </summary>
    /// <param name="type">C# type</param>
    /// <returns>SqlDbType</returns>
    public static class SqlParameterValueConverter
    {
        public static DbType? ToDbType(SqlDbType sqlDbType)
        {
            return sqlDbType switch
            {
                SqlDbType.Int => DbType.Int32,
                SqlDbType.BigInt => DbType.Int64,
                SqlDbType.SmallInt => DbType.Int16,
                SqlDbType.TinyInt => DbType.Byte,
                SqlDbType.Bit => DbType.Boolean,
                SqlDbType.Decimal => DbType.Decimal,
                SqlDbType.Money => DbType.Decimal,
                SqlDbType.SmallMoney => DbType.Decimal,
                SqlDbType.Float => DbType.Double,
                SqlDbType.Real => DbType.Single,
                SqlDbType.Date => DbType.Date,
                SqlDbType.DateTime => DbType.DateTime,
                SqlDbType.DateTime2 => DbType.DateTime2,
                SqlDbType.DateTimeOffset => DbType.DateTimeOffset,
                SqlDbType.Time => DbType.Time,
                SqlDbType.UniqueIdentifier => DbType.Guid,
                SqlDbType.NVarChar => DbType.String,
                SqlDbType.VarChar => DbType.String,
                SqlDbType.NChar => DbType.StringFixedLength,
                SqlDbType.Char => DbType.AnsiStringFixedLength,
                SqlDbType.Xml => DbType.Xml,
                _ => null
            };
        }

        public static object? Convert(object? value, SqlParameter parameter)
        {
            if (value is null)
            {
                return DBNull.Value;
            }

            switch (Type.GetTypeCode(value.GetType()))
            {
                case TypeCode.DateTime:
                    return ((DateTime)value).ToString(FormatDataType.DateTimeFormat);
                case TypeCode.Boolean:
                    return ((bool)value) ? FormatDataType.BooleanTrueFormat : FormatDataType.BooleanFalseFormat;
                case TypeCode.String:
                    return value;
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return string.Format(FormatDataType.FloatFormat, value);
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    return string.Format(FormatDataType.NumberFormat, value);
                case TypeCode.Object:
                    if (value is Guid guid)
                        return guid.ToString(FormatDataType.GuidFormat);
                    if (value is DateTimeOffset dto)
                        return dto.ToString(FormatDataType.DateTimeFormat);
                    if (value is TimeSpan timeSpan)
                        return timeSpan.ToString(FormatDataType.TimeSpanFormat);
                    break;
            }
            return value;
        }
    }

    /// <summary>
    /// Helper to flatten object properties to DynamicParameters (supporting nested objects).
    /// </summary>
    public static class ObjectFlattener
    {
        public static DynamicParameters ToDynamicParameters(object? obj)
        {
            var parameters = new DynamicParameters();
            AddProperties(parameters, obj);
            return parameters;
        }

        private static void AddProperties(DynamicParameters parameters, object? obj)
        {
            if (obj == null) return;

            var props = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                var value = prop.GetValue(obj);

                // skip null → để SQL default
                if (value == null)
                {
                    parameters.Add("@" + prop.Name, null);
                    continue;
                }

                var type = prop.PropertyType;

                if (value is IEnumerable<string>)
                    continue;

                // nếu là object phức tạp → flatten tiếp
                if (type.IsClass && type != typeof(string))
                {
                    AddProperties(parameters, value);
                }
                else
                {
                    parameters.Add("@" + prop.Name, value);
                }
            }
        }
    }
}