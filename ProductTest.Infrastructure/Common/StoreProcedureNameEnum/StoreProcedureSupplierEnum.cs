using System.ComponentModel;

namespace ProductTest.Infrastructure.Common.StoreProcedureNameEnum
{
    /// <summary>
    /// Enum listing all supplier-related stored procedure names for type-safety and centralized management.
    /// The Description attribute is used to associate the exact stored procedure name with each value.
    /// </summary>
    public enum StoreProcedureSupplierEnum
    {
        /// <summary>
        /// Gets all suppliers with pagination (dbo.usp_GetAllSuppliers).
        /// </summary>
        [Description("usp_SupplierGetAll")]
        GetAllSuppliers,

        /// <summary>
        /// Gets supplier by its unique identifier (dbo.usp_GetSupplierById).
        /// </summary>
        [Description("usp_SupplierGetById")]
        GetSupplierById,

        /// <summary>
        /// Gets supplier by its product id (dbo.usp_GetSupplierByProductId).
        /// </summary>
        [Description("usp_SupplierGetByProductId")]
        GetSupplierByProductId,

        /// <summary>
        /// Gets supplier by its unique code (dbo.usp_GetSupplierByCode).
        /// </summary>
        [Description("usp_SupplierGetByCode")]
        GetSupplierByCode,

        /// <summary>
        /// Creates a new supplier in the database using the dbo.usp_CreateSupplier stored procedure.
        /// </summary>
        [Description("usp_SupplierCreate")]
        CreateSupplier,

        /// <summary>
        /// Updates an existing supplier in the database using the dbo.usp_UpdateSupplier stored procedure.
        /// </summary>
        [Description("usp_SupplierUpdate")]
        UpdateSupplier,

        /// <summary>
        /// Soft deletes a supplier by setting IsActive = false (dbo.usp_DeleteSupplier).
        /// </summary>
        [Description("usp_SupplierDelete")]
        DeleteSupplier
    }

    /// <summary>
    /// Extension methods for StoreProcedureSupplierEnum to convert enum values to their actual stored procedure string.
    /// </summary>
    public static class SupplierStoredProcedureNameEnumExtensions
    {
        /// <summary>
        /// Returns the actual stored procedure name (including "dbo." prefix) for the enum value.
        /// </summary>
        public static string ToProcedureString(this StoreProcedureSupplierEnum procEnum)
        {
            var type = typeof(StoreProcedureSupplierEnum);
            var memInfo = type.GetMember(procEnum.ToString());
            var descriptionAttr = memInfo.Length > 0
                ? Attribute.GetCustomAttribute(memInfo[0], typeof(DescriptionAttribute)) as DescriptionAttribute
                : null;

            var procName = descriptionAttr?.Description ?? procEnum.ToString();
            return $"dbo.{procName}";
        }
    }
}