using AutoMapper;
using ProductTest.Domain.Entities;
using ProductTest.Application.DTOs.Request.Product;
using ProductTest.Application.DTOs.Response.Product;

namespace ProductTest.Application.Common.Mapping;

public sealed class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductDto>();

        CreateMap<CreateProductRequest, Product>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Code, o => o.Ignore())
            .ForMember(d => d.Supplier, o => o.Ignore())
            .ForMember(d => d.CartItems, o => o.Ignore())
            .ForMember(d => d.OrderItems, o => o.Ignore())
            .ForMember(d => d.ProductRatings, o => o.Ignore())
            .ForMember(d => d.CreatedAt, o => o.Ignore())
            .ForMember(d => d.UpdatedAt, o => o.Ignore());

        CreateMap<UpdateProductBody, Product>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Code, o => o.Ignore())
            .ForMember(d => d.Supplier, o => o.Ignore())
            .ForMember(d => d.CartItems, o => o.Ignore())
            .ForMember(d => d.OrderItems, o => o.Ignore())
            .ForMember(d => d.ProductRatings, o => o.Ignore())
            .ForMember(d => d.CreatedAt, o => o.Ignore());

        CreateMap<UpdateProductBody, ProductDto>();
        CreateMap<CreateProductRequest, ProductDto>();
    }
}
