using System.ComponentModel;

namespace ProductTest.Infrastructure.Common.StoreProcedureNameEnum;

public enum StoreProcedureUserPermissionEnum
{
    [Description("usp_UserPermissionCreate")]
    UserPermissionCreate,

    [Description("usp_UserPermissionDelete")]
    UserPermissionDelete,

    [Description("usp_UserPermissionGetByUserId")]
    UserPermissionGetByUserId,

    [Description("usp_UserPermissionGetByPermissionId")]
    UserPermissionGetByPermissionId
}

public static class StoreProcedureUserPermissionEnumExtensions
{
    public static string ToProcedureString(this StoreProcedureUserPermissionEnum procEnum)
    {
        var type = typeof(StoreProcedureUserPermissionEnum);
        var memInfo = type.GetMember(procEnum.ToString());
        var descriptionAttr = memInfo.Length > 0
            ? Attribute.GetCustomAttribute(memInfo[0], typeof(DescriptionAttribute)) as DescriptionAttribute
            : null;

        var procName = descriptionAttr?.Description ?? procEnum.ToString();
        return $"dbo.{procName}";
    }
}

