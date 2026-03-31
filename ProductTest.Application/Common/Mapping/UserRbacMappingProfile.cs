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

        CreateMap<UserRole, UserRoleDto>();

        CreateMap<UserPermission, UserPermissionDto>();

        CreateMap<RolePermission, RolePermissionDto>();

        CreateMap<UserToken, UserTokenDto>();

        CreateMap<LoginRequest, GetUserByEmailRequest>();

        CreateMap<RegisterRequest, RegisterUserRequest>();

        CreateMap<LogoutRequest, LogoutUserRequest>();

        CreateMap<RefreshTokenRequest, RefreshTokenUserRequest>();
    }
}
