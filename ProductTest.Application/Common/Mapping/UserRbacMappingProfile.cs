using AutoMapper;
using ProductTest.Application.DTOs.Request.Auth;
using ProductTest.Application.DTOs.Request.User;
using ProductTest.Application.DTOs.Response.Permission;
using ProductTest.Application.DTOs.Response.Role;
using ProductTest.Application.DTOs.Response.RolePermission;
using ProductTest.Application.DTOs.Response.User;
using ProductTest.Application.DTOs.Response.UserPermission;
using ProductTest.Application.DTOs.Response.UserRole;
using ProductTest.Application.DTOs.Response.UserToken;
using ProductTest.Domain.Entities;

namespace ProductTest.Application.Common.Mapping;

public sealed class UserRbacMappingProfile : Profile
{
    public UserRbacMappingProfile()
    {
        CreateMap<User, UserDto>();

        CreateMap<Role, RoleDto>();

        CreateMap<Permission, PermissionDto>();

        CreateMap<UserRole, UserRoleDto>()
            .ForMember(d => d.RoleCode, o => o.MapFrom(s => s.Role != null ? s.Role.Code : string.Empty))
            .ForMember(d => d.RoleName, o => o.MapFrom(s => s.Role != null ? s.Role.Name : string.Empty));

        CreateMap<UserPermission, UserPermissionDto>()
            .ForMember(d => d.PermissionCode, o => o.MapFrom(s => s.Permission != null ? s.Permission.Code : string.Empty))
            .ForMember(d => d.PermissionName, o => o.MapFrom(s => s.Permission != null ? s.Permission.Name : string.Empty));

        CreateMap<RolePermission, RolePermissionDto>()
            .ForMember(d => d.PermissionCode, o => o.MapFrom(s => s.Permission != null ? s.Permission.Code : string.Empty))
            .ForMember(d => d.PermissionName, o => o.MapFrom(s => s.Permission != null ? s.Permission.Name : string.Empty));

        CreateMap<UserToken, UserTokenDto>();

        CreateMap<LoginRequest, GetUserByEmailRequest>();

        CreateMap<RegisterRequest, RegisterUserRequest>();

        CreateMap<LogoutRequest, LogoutUserRequest>();

        CreateMap<RefreshTokenRequest, RefreshTokenUserRequest>();
    }
}
