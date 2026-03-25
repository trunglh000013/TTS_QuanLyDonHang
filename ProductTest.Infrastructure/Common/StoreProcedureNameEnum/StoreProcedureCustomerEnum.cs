using System.ComponentModel;

namespace ProductTest.Infrastructure.Common.StoreProcedureNameEnum
{
    /// <summary>
    /// Enum listing all customer-related stored procedure names for type-safety and centralized management.
    /// The Description attribute is used to associate the exact stored procedure name with each value.
    /// </summary>
    public enum StoreProcedureCustomerEnum
    {
        /// <summary>
        /// Get all active customers, paged list.
        /// </summary>
        [Description("usp_CustomerGetAll")]
        CustomerGetAll,

        /// <summary>
        /// Get a customer by their Id.
        /// </summary>
        [Description("usp_CustomerGetById")]
        CustomerGetById,

        /// <summary>
        /// Get a customer by their unique code.
        /// </summary>
        [Description("usp_CustomerGetByCode")]
        CustomerGetByCode,

        /// <summary>
        /// Create a new customer.
        /// </summary>
        [Description("usp_CustomerCreate")]
        CustomerCreate,

        /// <summary>
        /// Update an existing customer.
        /// </summary>
        [Description("usp_CustomerUpdate")]
        CustomerUpdate,

        /// <summary>
        /// Soft-delete a customer (sets IsActive = 0).
        /// </summary>
        [Description("usp_CustomerDelete")]
        CustomerDelete
    }

    /// <summary>
    /// Extension methods for StoreProcedureCustomerEnum to convert enum values to their actual stored procedure string.
    /// </summary>
    public static class StoreProcedureCustomerEnumExtensions
    {
        /// <summary>
        /// Returns the actual stored procedure name (including "dbo." prefix) for the enum value.
        /// </summary>
        public static string ToProcedureString(this StoreProcedureCustomerEnum procEnum)
        {
            var type = typeof(StoreProcedureCustomerEnum);
            var memInfo = type.GetMember(procEnum.ToString());
            var descriptionAttr = memInfo.Length > 0
                ? Attribute.GetCustomAttribute(memInfo[0], typeof(DescriptionAttribute)) as DescriptionAttribute
                : null;

            var procName = descriptionAttr?.Description ?? procEnum.ToString();
            return $"dbo.{procName}";
        }
    }
}