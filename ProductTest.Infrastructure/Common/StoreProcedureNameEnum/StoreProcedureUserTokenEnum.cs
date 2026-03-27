using System.ComponentModel;

namespace ProductTest.Infrastructure.Common.StoreProcedureNameEnum;

public enum StoreProcedureUserTokenEnum
{
    [Description("usp_UserTokenGetByRefreshToken")]
    UserTokenGetByRefreshToken,

    [Description("usp_UserTokenGetByUserId")]
    UserTokenGetByUserId
}

public static class StoreProcedureUserTokenEnumExtensions
{
    public static string ToProcedureString(this StoreProcedureUserTokenEnum procEnum)
    {
        var type = typeof(StoreProcedureUserTokenEnum);
        var memInfo = type.GetMember(procEnum.ToString());
        var descriptionAttr = memInfo.Length > 0
            ? Attribute.GetCustomAttribute(memInfo[0], typeof(DescriptionAttribute)) as DescriptionAttribute
            : null;

        var procName = descriptionAttr?.Description ?? procEnum.ToString();
        return $"dbo.{procName}";
    }
}
