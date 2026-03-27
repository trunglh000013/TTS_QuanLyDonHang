using System.ComponentModel;

namespace ProductTest.Infrastructure.Common.StoreProcedureNameEnum;

public enum StoreProcedureUserRoleEnum
{
    [Description("usp_UserRoleCreate")]
    UserRoleCreate,

    [Description("usp_UserRoleDelete")]
    UserRoleDelete,

    [Description("usp_UserRoleGetByUserId")]
    UserRoleGetByUserId,

    [Description("usp_UserRoleGetByRoleId")]
    UserRoleGetByRoleId
}

public static class StoreProcedureUserRoleEnumExtensions
{
    public static string ToProcedureString(this StoreProcedureUserRoleEnum procEnum)
    {
        var type = typeof(StoreProcedureUserRoleEnum);
        var memInfo = type.GetMember(procEnum.ToString());
        var descriptionAttr = memInfo.Length > 0
            ? Attribute.GetCustomAttribute(memInfo[0], typeof(DescriptionAttribute)) as DescriptionAttribute
            : null;

        var procName = descriptionAttr?.Description ?? procEnum.ToString();
        return $"dbo.{procName}";
    }
}

