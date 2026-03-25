using System.ComponentModel;

namespace ProductTest.Infrastructure.Common.StoreProcedureNameEnum
{
    /// <summary>
    /// Enum listing all order-related stored procedure names for type-safety and centralized management.
    /// The Description attribute is used to associate the exact stored procedure name with each value.
    /// </summary>
    public enum StoreProcedureOrderEnum
    {
        /// <summary>
        /// Gets all orders with pagination (dbo.usp_OrderGetAll).
        /// </summary>
        [Description("usp_OrderGetAll")]
        GetAll,

        /// <summary>
        /// Gets a single order by Id (dbo.usp_OrderGetById).
        /// </summary>
        [Description("usp_OrderGetById")]
        GetById,

        /// <summary>
        /// Gets a single order by Code (dbo.usp_OrderGetByCode).
        /// </summary>
        [Description("usp_OrderGetByCode")]
        GetByCode,

        /// <summary>
        /// Gets all orders by CustomerId with pagination (dbo.usp_OrderGetByCustomerId).
        /// </summary>
        [Description("usp_OrderGetByCustomerId")]
        GetByCustomerId,

        /// <summary>
        /// Creates a new order in the database using the dbo.usp_OrderCreate stored procedure.
        /// </summary>
        [Description("usp_OrderCreate")]
        Create,

        /// <summary>
        /// Creates a new order in the database using the dbo.usp_OrderCreateByCartId stored procedure.
        /// </summary>
        [Description("usp_OrderCreateByCartId")]
        CreateByCartId,

        /// <summary>
        /// Updates an existing order in the database using the dbo.usp_OrderUpdate stored procedure.
        /// </summary>
        [Description("usp_OrderUpdate")]
        Update,

        /// <summary>
        /// Soft deletes an order by setting IsActive = false (dbo.usp_OrderDelete).
        /// </summary>
        [Description("usp_OrderDelete")]
        Delete,

        /// <summary>
        /// Gets the detail of an order by OrderId (dbo.usp_OrderGetDetail).
        /// </summary>
        [Description("usp_OrderGetDetail")]
        GetDetail
    }

    /// <summary>
    /// Extension methods for StoreProcedureOrderEnum to convert enum values to their actual stored procedure string.
    /// </summary>
    public static class StoreProcedureOrderEnumExtensions
    {
        /// <summary>
        /// Returns the actual stored procedure name (including "dbo." prefix) for the enum value.
        /// </summary>
        public static string ToProcedureString(this StoreProcedureOrderEnum procEnum)
        {
            var type = typeof(StoreProcedureOrderEnum);
            var memInfo = type.GetMember(procEnum.ToString());
            var descriptionAttr = memInfo.Length > 0
                ? Attribute.GetCustomAttribute(memInfo[0], typeof(DescriptionAttribute)) as DescriptionAttribute
                : null;

            var procName = descriptionAttr?.Description ?? procEnum.ToString();
            return $"dbo.{procName}";
        }
    }
}