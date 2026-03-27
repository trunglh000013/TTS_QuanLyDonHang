using System.ComponentModel;

namespace ProductTest.Infrastructure.Common.StoreProcedureNameEnum;

public enum StoreProcedurePermissionEnum
{
    [Description("usp_PermissionGetAll")]
    PermissionGetAll,

    [Description("usp_PermissionGetById")]
    PermissionGetById,

    [Description("usp_PermissionGetByCode")]
    PermissionGetByCode,

    [Description("usp_PermissionCreate")]
    PermissionCreate,

    [Description("usp_PermissionUpdate")]
    PermissionUpdate,

    [Description("usp_PermissionDelete")]
    PermissionDelete
}

public static class StoreProcedurePermissionEnumExtensions
{
    public static string ToProcedureString(this StoreProcedurePermissionEnum procEnum)
    {
        var type = typeof(StoreProcedurePermissionEnum);
        var memInfo = type.GetMember(procEnum.ToString());
        var descriptionAttr = memInfo.Length > 0
            ? Attribute.GetCustomAttribute(memInfo[0], typeof(DescriptionAttribute)) as DescriptionAttribute
            : null;

        var procName = descriptionAttr?.Description ?? procEnum.ToString();
        return $"dbo.{procName}";
    }
}

