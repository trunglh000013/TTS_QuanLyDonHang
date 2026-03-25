using System.ComponentModel;

namespace ProductTest.Infrastructure.Common.StoreProcedureNameEnum
{
    /// <summary>
    /// Enum listing all product-related stored procedure names for type-safety and centralized management.
    /// The Description attribute is used to associate the exact stored procedure name with each value.
    /// </summary>
    public enum StoreProcedureProductEnum
    {
        /// <summary>
        /// Creates a new product in the database using the dbo.usp_CreateProduct stored procedure.
        /// </summary>
        [Description("usp_ProductCreate")]
        Create,

        /// <summary>
        /// Updates an existing product in the database using the dbo.usp_UpdateProduct stored procedure.
        /// </summary>
        [Description("usp_ProductUpdate")]
        Update,

        /// <summary>
        /// Searches for products by term with pagination using the dbo.usp_SearchProducts stored procedure.
        /// </summary>
        [Description("usp_ProductSearch")]
        Search,

        /// <summary>
        /// Filters products with optional criteria and pagination (dbo.usp_FilterProducts).
        /// </summary>
        [Description("usp_ProductFilter")]
        Filter,

        /// <summary>
        /// Gets product by its unique identifier (dbo.usp_GetProductById).
        /// </summary>
        [Description("usp_ProductGetById")]
        GetById,

        /// <summary>
        /// Gets product by its name (dbo.usp_GetProductByName).
        /// </summary>
        [Description("usp_ProductGetByName")]
        GetByName,

        /// <summary>
        /// Gets product(s) by category with pagination (dbo.usp_GetProductByCategory).
        /// </summary>
        [Description("usp_ProductGetByCategory")]
        GetByCategory,

        /// <summary>
        /// Soft deletes a product by setting IsActive = false (dbo.usp_DeleteProduct).
        /// </summary>
        [Description("usp_ProductDelete")]
        Delete
    }

    /// <summary>
    /// Extension methods for ProductStoredProcedureNameEnum to convert enum values to their actual stored procedure string.
    /// </summary>
    public static class ProductStoredProcedureNameEnumExtensions
    {
        /// <summary>
        /// Returns the actual stored procedure name (including "dbo." prefix) for the enum value.
        /// </summary>
        public static string ToProcedureString(this StoreProcedureProductEnum procEnum)
        {
            var type = typeof(StoreProcedureProductEnum);
            var memInfo = type.GetMember(procEnum.ToString());
            var descriptionAttr = memInfo.Length > 0
                ? Attribute.GetCustomAttribute(memInfo[0], typeof(DescriptionAttribute)) as DescriptionAttribute
                : null;

            var procName = descriptionAttr?.Description ?? procEnum.ToString();
            return $"dbo.{procName}";
        }
    }
}