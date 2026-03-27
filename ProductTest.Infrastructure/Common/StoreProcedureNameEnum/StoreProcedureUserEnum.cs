using System.ComponentModel;

namespace ProductTest.Infrastructure.Common.StoreProcedureNameEnum;

public enum StoreProcedureUserEnum
{
    [Description("usp_UserGetAll")]
    UserGetAll,

    [Description("usp_UserGetById")]
    UserGetById,

    [Description("usp_UserGetByEmail")]
    UserGetByEmail,

    [Description("usp_UserCreate")]
    UserCreate,

    [Description("usp_UserUpdate")]
    UserUpdate,

    [Description("usp_UserDelete")]
    UserDelete,

    [Description("usp_UserGrantRole")]
    UserGrantRole,

    [Description("usp_UserRevokeRole")]
    UserRevokeRole,

    [Description("usp_UserGrantPermission")]
    UserGrantPermission,

    [Description("usp_UserRevokePermission")]
    UserRevokePermission,

    [Description("usp_UserLogin")]
    UserLogin,

    [Description("usp_UserLogout")]
    UserLogout,

    [Description("usp_UserRefreshToken")]
    UserRefreshToken,

    [Description("usp_UserRegister")]
    UserRegister
}

public static class StoreProcedureUserEnumExtensions
{
    public static string ToProcedureString(this StoreProcedureUserEnum procEnum)
    {
        var type = typeof(StoreProcedureUserEnum);
        var memInfo = type.GetMember(procEnum.ToString());
        var descriptionAttr = memInfo.Length > 0
            ? Attribute.GetCustomAttribute(memInfo[0], typeof(DescriptionAttribute)) as DescriptionAttribute
            : null;

        var procName = descriptionAttr?.Description ?? procEnum.ToString();
        return $"dbo.{procName}";
    }
}

