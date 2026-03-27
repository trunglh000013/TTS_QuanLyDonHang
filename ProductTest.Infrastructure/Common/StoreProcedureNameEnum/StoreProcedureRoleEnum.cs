using System.ComponentModel;

namespace ProductTest.Infrastructure.Common.StoreProcedureNameEnum;

public enum StoreProcedureRoleEnum
{
    [Description("usp_RoleGetAll")]
    RoleGetAll,

    [Description("usp_RoleGetById")]
    RoleGetById,

    [Description("usp_RoleGetByCode")]
    RoleGetByCode,

    [Description("usp_RoleCreate")]
    RoleCreate,

    [Description("usp_RoleUpdate")]
    RoleUpdate,

    [Description("usp_RoleDelete")]
    RoleDelete,

    [Description("usp_RoleGrantPermission")]
    RoleGrantPermission,

    [Description("usp_RoleRevokePermission")]
    RoleRevokePermission
}

public static class StoreProcedureRoleEnumExtensions
{
    public static string ToProcedureString(this StoreProcedureRoleEnum procEnum)
    {
        var type = typeof(StoreProcedureRoleEnum);
        var memInfo = type.GetMember(procEnum.ToString());
        var descriptionAttr = memInfo.Length > 0
            ? Attribute.GetCustomAttribute(memInfo[0], typeof(DescriptionAttribute)) as DescriptionAttribute
            : null;

        var procName = descriptionAttr?.Description ?? procEnum.ToString();
        return $"dbo.{procName}";
    }
}

