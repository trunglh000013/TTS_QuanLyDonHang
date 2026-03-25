using AutoMapper;
using ProductTest.Application.DTOs.Request.Cart;
using ProductTest.Application.DTOs.Response.Cart;
using ProductTest.Domain.Entities;
namespace ProductTest.Application.Common.Mapping;

public sealed class CartMappingProfile : Profile
{
    public CartMappingProfile()
    {
        CreateMap<CartItem, CartItemDto>();

        CreateMap<Cart, CartDto>()
            .ForMember(d => d.Items, o => o.MapFrom(s => s.Items));

        CreateMap<CreateCartRequest, Cart>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Code, o => o.Ignore())
            .ForMember(d => d.CreatedAt, o => o.Ignore())
            .ForMember(d => d.UpdatedAt, o => o.Ignore())
            .ForMember(d => d.IsActive, o => o.Ignore())
            .ForMember(d => d.Customer, o => o.Ignore())
            .ForMember(d => d.Items, o => o.Ignore());

        CreateMap<AddItemCartRequest, CartItem>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Code, o => o.Ignore())
            .ForMember(d => d.Price, o => o.Ignore())
            .ForMember(d => d.CreatedAt, o => o.Ignore())
            .ForMember(d => d.UpdatedAt, o => o.Ignore())
            .ForMember(d => d.IsActive, o => o.Ignore())
            .ForMember(d => d.Cart, o => o.Ignore())
            .ForMember(d => d.Product, o => o.Ignore());

        CreateMap<DeleteCartRequest, Cart>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Code, o => o.Ignore())
            .ForMember(d => d.CreatedAt, o => o.Ignore())
            .ForMember(d => d.UpdatedAt, o => o.Ignore())
            .ForMember(d => d.IsActive, o => o.Ignore())
            .ForMember(d => d.Customer, o => o.Ignore())
            .ForMember(d => d.Items, o => o.Ignore());
    }
}