using System.ComponentModel;

namespace ProductTest.Infrastructure.Common.StoreProcedureNameEnum;

public enum StoreProcedureRolePermissionEnum
{
    [Description("usp_RolePermissionCreate")]
    RolePermissionCreate,

    [Description("usp_RolePermissionDelete")]
    RolePermissionDelete,

    [Description("usp_RolePermissionGetByRoleId")]
    RolePermissionGetByRoleId,

    [Description("usp_RolePermissionGetByPermissionId")]
    RolePermissionGetByPermissionId
}

public static class StoreProcedureRolePermissionEnumExtensions
{
    public static string ToProcedureString(this StoreProcedureRolePermissionEnum procEnum)
    {
        var type = typeof(StoreProcedureRolePermissionEnum);
        var memInfo = type.GetMember(procEnum.ToString());
        var descriptionAttr = memInfo.Length > 0
            ? Attribute.GetCustomAttribute(memInfo[0], typeof(DescriptionAttribute)) as DescriptionAttribute
            : null;

        var procName = descriptionAttr?.Description ?? procEnum.ToString();
        return $"dbo.{procName}";
    }
}

