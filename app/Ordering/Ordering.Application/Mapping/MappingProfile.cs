using AutoMapper;
using Ordering.DataAccess.Entities;
using Ordering.Domain.Models;

namespace Ordering.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<OrderEntity, Order>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

        CreateMap<OrderItemEntity, OrderItem>();

        CreateMap<Order, OrderEntity>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.UserId));

        CreateMap<OrderItem, OrderItemEntity>();

        CreateMap<User, UserEntity>();
        CreateMap<UserEntity, User>();
    }
}
