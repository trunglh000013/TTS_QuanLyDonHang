using System.Data;

namespace ProductTest.Application.Abstractions.Helpers
{
    /// <summary>
    /// General-purpose interface for executing stored procedures against a database.
    /// </summary>
    public interface IStoreProcedureRunner
    {
        /// <summary>
        /// Executes a stored procedure and maps the result to a list of type <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="TResult">The type to map the result set to. Must be a class.</typeparam>
        /// <param name="storeProcedureName">The name of the stored procedure to execute. Can include or omit schema ('dbo.').</param>
        /// <param name="parameters">Object (typically a DTO or anonymous type) whose properties are mapped to SQL parameters.</param>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A list of objects of type <typeparamref name="TResult"/> mapped from the result set.</returns>
        Task<List<TResult>> ExecuteProcedureAsync<TResult>(
            string storeProcedureName,
            object parameters,
            CancellationToken cancellationToken = default
        ) where TResult : class;

        /// <summary>
        /// Executes a stored procedure and returns the result as a DataSet.
        /// </summary>
        /// <param name="storeProcedureName">The name of the stored procedure to execute. Can include or omit schema ('dbo.').</param>
        /// <param name="parameters">Object (typically a DTO or anonymous type) whose properties are mapped to SQL parameters.</param>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A DataSet containing the result of the stored procedure.</returns>
        Task<DataSet> ExecuteProcedureToDataSetAsync(
            string storeProcedureName,
            object parameters,
            CancellationToken cancellationToken = default
        );

        /// <summary>
        /// Executes a stored procedure that does not return any entities (typically for INSERT, UPDATE, or DELETE operations).
        /// </summary>
        /// <param name="storeProcedureName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">Object (typically a DTO or anonymous type) whose properties are mapped to SQL parameters.</param>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>The number of rows affected by the operation.</returns>
        Task<int> ExecuteNonQueryAsync(
            string storeProcedureName,
            object parameters,
            CancellationToken cancellationToken = default
        );
    }
}