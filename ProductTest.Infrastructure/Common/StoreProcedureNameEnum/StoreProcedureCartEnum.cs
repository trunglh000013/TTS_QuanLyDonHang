using System.ComponentModel;

namespace ProductTest.Infrastructure.Common.StoreProcedureNameEnum
{
    /// <summary>
    /// Enum listing all cart-related stored procedure names for type-safety and centralized management.
    /// The Description attribute is used to associate the exact stored procedure name with each value.
    /// </summary>
    public enum StoreProcedureCartEnum
    {
        /// <summary>
        /// Gets all cart items by customer id (dbo.usp_CartGetAllItemByCustomerId).
        /// </summary>
        [Description("usp_CartGetAllItemByCustomerId")]
        CartGetAllItemByCustomerId,

        /// <summary>
        /// Creates a new cart in the database using the dbo.usp_CartCreate stored procedure.
        /// </summary>
        [Description("usp_CartCreate")]
        CartCreate,

        /// <summary>
        /// Adds an item to the cart using the dbo.usp_CartAddItem stored procedure.
        /// </summary>
        [Description("usp_CartAddItem")]
        CartAddItem,

        /// <summary>
        /// Deletes an item from the cart using the dbo.usp_CartDeleteItem stored procedure.
        /// </summary>
        [Description("usp_CartDeleteItem")]
        CartDeleteItem,

        /// <summary>
        /// Deletes a cart using the dbo.usp_CartDelete stored procedure.
        /// </summary>
        [Description("usp_CartDelete")]
        CartDelete,
    }

    /// <summary>
    /// Extension methods for StoreProcedureCartEnum to convert enum values to their actual stored procedure string.
    /// </summary>
    public static class StoreProcedureCartEnumExtensions
    {
        /// <summary>
        /// Returns the actual stored procedure name (including "dbo." prefix) for the enum value.
        /// </summary>
        public static string ToProcedureString(this StoreProcedureCartEnum procEnum)
        {
            var type = typeof(StoreProcedureCartEnum);
            var memInfo = type.GetMember(procEnum.ToString());
            var descriptionAttr = memInfo.Length > 0
                ? Attribute.GetCustomAttribute(memInfo[0], typeof(DescriptionAttribute)) as DescriptionAttribute
                : null;

            var procName = descriptionAttr?.Description ?? procEnum.ToString();
            return $"dbo.{procName}";
        }
    }
}