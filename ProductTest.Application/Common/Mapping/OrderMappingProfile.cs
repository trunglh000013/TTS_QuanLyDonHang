using AutoMapper;
using ProductTest.Application.DTOs.Request.Order;
using ProductTest.Application.DTOs.Response.Order;
using ProductTest.Domain.Entities;

namespace ProductTest.Application.Common.Mapping;

public sealed class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<OrderItem, OrderItemDto>();

        CreateMap<Order, OrderDto>()
            .ForMember(d => d.Items, o => o.MapFrom(s => s.Items));

        CreateMap<CreateOrderRequest, Order>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Code, o => o.Ignore())
            .ForMember(d => d.Status, o => o.MapFrom(_ => "Placed"))
            .ForMember(d => d.TotalAmount, o => o.Ignore())
            .ForMember(d => d.CreatedAt, o => o.Ignore())
            .ForMember(d => d.UpdatedAt, o => o.Ignore())
            .ForMember(d => d.IsActive, o => o.MapFrom(_ => true))
            .ForMember(d => d.Customer, o => o.Ignore())
            .ForMember(d => d.Cart, o => o.Ignore())
            .ForMember(d => d.Items, o => o.Ignore());

        CreateMap<UpdateOrderRequest, Order>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Code, o => o.Ignore())
            .ForMember(d => d.Status, o => o.MapFrom(_ => "Placed"))
            .ForMember(d => d.TotalAmount, o => o.Ignore())
            .ForMember(d => d.CreatedAt, o => o.Ignore())
            .ForMember(d => d.UpdatedAt, o => o.Ignore())
            .ForMember(d => d.IsActive, o => o.MapFrom(_ => true))
            .ForMember(d => d.Customer, o => o.Ignore())
            .ForMember(d => d.Cart, o => o.Ignore())
            .ForMember(d => d.Items, o => o.Ignore());

        CreateMap<UpdateOrderBody, OrderItem>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Code, o => o.Ignore())
            .ForMember(d => d.OrderId, o => o.Ignore())
            .ForMember(d => d.CreatedAt, o => o.Ignore())
            .ForMember(d => d.UpdatedAt, o => o.Ignore())
            .ForMember(d => d.IsActive, o => o.Ignore())
            .ForMember(d => d.Order, o => o.Ignore())
            .ForMember(d => d.Product, o => o.Ignore());
    }
}
