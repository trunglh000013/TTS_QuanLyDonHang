using AutoMapper;
using ProductTest.Application.DTOs.Request.Supplier;
using ProductTest.Application.DTOs.Response.Supplier;
using ProductTest.Domain.Entities;

namespace ProductTest.Application.Common.Mapping;

public sealed class SupplierMappingProfile : Profile
{
    public SupplierMappingProfile()
    {
        CreateMap<Supplier, SupplierDto>();

        CreateMap<CreateSupplierRequest, Supplier>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Products, o => o.Ignore())
            .ForMember(d => d.CreatedAt, o => o.Ignore())
            .ForMember(d => d.UpdatedAt, o => o.Ignore())
            .ForMember(d => d.IsActive, o => o.Ignore());

        CreateMap<UpdateSupplierBody, Supplier>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Code, o => o.Ignore())
            .ForMember(d => d.Products, o => o.Ignore())
            .ForMember(d => d.CreatedAt, o => o.Ignore())
            .ForMember(d => d.UpdatedAt, o => o.Ignore())
            .ForMember(d => d.IsActive, o => o.Ignore());
    }
}
