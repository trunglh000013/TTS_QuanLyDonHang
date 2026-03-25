using AutoMapper;
using ProductTest.Application.DTOs.Request.ProductRating;
using ProductTest.Application.DTOs.Response.ProductRating;
using ProductTest.Domain.Entities;

namespace ProductTest.Application.Common.Mapping;

public sealed class ProductRatingMappingProfile : Profile
{
    public ProductRatingMappingProfile()
    {
        CreateMap<ProductRating, ProductRatingDto>();

        CreateMap<CreateProductRatingRequest, ProductRating>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Code, o => o.Ignore())
            .ForMember(d => d.CreatedAt, o => o.Ignore())
            .ForMember(d => d.UpdatedAt, o => o.Ignore())
            .ForMember(d => d.IsActive, o => o.Ignore())
            .ForMember(d => d.Product, o => o.Ignore())
            .ForMember(d => d.Customer, o => o.Ignore());

        CreateMap<UpdateProductRatingBody, ProductRating>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Code, o => o.Ignore())
            .ForMember(d => d.ProductId, o => o.Ignore())
            .ForMember(d => d.CustomerId, o => o.Ignore())
            .ForMember(d => d.CreatedAt, o => o.Ignore())
            .ForMember(d => d.UpdatedAt, o => o.Ignore())
            .ForMember(d => d.IsActive, o => o.Ignore())
            .ForMember(d => d.Product, o => o.Ignore())
            .ForMember(d => d.Customer, o => o.Ignore());
    }
}
