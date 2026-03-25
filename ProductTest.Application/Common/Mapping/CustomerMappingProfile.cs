using AutoMapper;
using ProductTest.Application.DTOs.Request.Customer;
using ProductTest.Application.DTOs.Response.Customer;
using ProductTest.Domain.Entities;

namespace ProductTest.Application.Common.Mapping;

public sealed class CustomerMappingProfile : Profile
{
    public CustomerMappingProfile()
    {
        CreateMap<Customer, CustomerDto>();

        CreateMap<CreateCustomerRequest, Customer>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Carts, o => o.Ignore())
            .ForMember(d => d.Orders, o => o.Ignore())
            .ForMember(d => d.ProductRatings, o => o.Ignore())
            .ForMember(d => d.CreatedAt, o => o.Ignore())
            .ForMember(d => d.UpdatedAt, o => o.Ignore())
            .ForMember(d => d.IsActive, o => o.Ignore());

        CreateMap<UpdateCustomerBody, Customer>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Code, o => o.Ignore())
            .ForMember(d => d.Carts, o => o.Ignore())
            .ForMember(d => d.Orders, o => o.Ignore())
            .ForMember(d => d.ProductRatings, o => o.Ignore())
            .ForMember(d => d.CreatedAt, o => o.Ignore())
            .ForMember(d => d.UpdatedAt, o => o.Ignore())
            .ForMember(d => d.IsActive, o => o.Ignore());
    }
}
