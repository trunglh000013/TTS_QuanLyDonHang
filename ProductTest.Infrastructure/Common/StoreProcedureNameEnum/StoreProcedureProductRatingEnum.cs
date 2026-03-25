using System.ComponentModel;

namespace ProductTest.Infrastructure.Common.StoreProcedureNameEnum
{
    /// <summary>
    /// Enum listing all product rating-related stored procedure names for type-safety and centralized management.
    /// The Description attribute associates the exact stored procedure name with each enum value.
    /// </summary>
    public enum StoreProcedureProductRatingEnum
    {
        /// <summary>
        /// Creates a new product rating in the database (dbo.usp_ProductRatingCreate).
        /// </summary>
        [Description("usp_ProductRatingCreate")]
        Create,

        /// <summary>
        /// Updates an existing product rating in the database (dbo.usp_ProductRatingUpdate).
        /// </summary>
        [Description("usp_ProductRatingUpdate")]
        Update,

        /// <summary>
        /// Gets product rating by its unique identifier (dbo.usp_ProductRatingGetById).
        /// </summary>
        [Description("usp_ProductRatingGetById")]
        GetById,

        /// <summary>
        /// Gets product ratings for a given product id, with pagination (dbo.usp_ProductRatingGetByProductId).
        /// </summary>
        [Description("usp_ProductRatingGetByProductId")]
        GetByProductId,

        /// <summary>
        /// Soft deletes a product rating by setting IsActive = false (dbo.usp_ProductRatingDelete).
        /// </summary>
        [Description("usp_ProductRatingDelete")]
        Delete
    }

    /// <summary>
    /// Extension methods for StoreProcedureProductRatingEnum to convert enum values to the actual stored procedure string.
    /// </summary>
    public static class StoreProcedureProductRatingEnumExtensions
    {
        /// <summary>
        /// Returns the actual stored procedure name (including "dbo." prefix) for the enum value.
        /// </summary>
        public static string ToProcedureString(this StoreProcedureProductRatingEnum procEnum)
        {
            var type = typeof(StoreProcedureProductRatingEnum);
            var memInfo = type.GetMember(procEnum.ToString());
            var descriptionAttr = memInfo.Length > 0
                ? Attribute.GetCustomAttribute(memInfo[0], typeof(DescriptionAttribute)) as DescriptionAttribute
                : null;

            var procName = descriptionAttr?.Description ?? procEnum.ToString();
            return $"dbo.{procName}";
        }
    }
}