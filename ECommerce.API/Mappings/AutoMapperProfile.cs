using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;

namespace ECommerce.API.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            ConfigureMappings<Product, ProductDTO, ProductCreateDTO, ProductUpdateDTO>();
            ConfigureMappings<ProductAggregate, ProductDTO, ProductCreateDTO, ProductUpdateDTO>();

            ConfigureMappings<Category, CategoryDTO, CategoryCreateDTO, CategoryUpdateDTO>();
            ConfigureMappings<CategoryAggregate, CategoryDTO, CategoryCreateDTO, CategoryUpdateDTO>();

            ConfigureMappings<Order, OrderDTO, OrderCreateDTO, OrderUpdateDTO>();
            ConfigureMappings<OrderAggregate, OrderDTO, OrderCreateDTO, OrderUpdateDTO>();

            ConfigureMappings<OrderItem, OrderItemDTO, OrderItemCreateDTO, OrderItemUpdateDTO>();
            ConfigureMappings<OrderItemAggregate, OrderItemDTO, OrderItemCreateDTO, OrderItemUpdateDTO>();

            ConfigureMappings<User, UserDTO, UserCreateDTO, UserUpdateDTO>();
            ConfigureMappings<UserAggregate, UserDTO, UserCreateDTO, UserUpdateDTO>();

            ConfigureMappings<Address, AddressDTO, AddressCreateDTO, AddressUpdateDTO>();
            ConfigureMappings<AddressAggregate, AddressDTO, AddressCreateDTO, AddressUpdateDTO>();

            ConfigureMappings<Product, ProductDTO, ProductStockChangeDTO, ProductPriceChangeDTO>();

            ConfigureMappings<CartItem, CartItemDTO, CartItemCreateDTO, CartItemUpdateDTO>();
            ConfigureMappings<CartItemAggregate, CartItemDTO, CartItemCreateDTO, CartItemUpdateDTO>();

            ConfigureMappings<Gender, GenderDTO, GenderCreateDTO, GenderUpdateDTO>();
            ConfigureMappings<GenderAggregate, GenderDTO, GenderCreateDTO, GenderUpdateDTO>();

            ConfigureMappings<Role, RoleDTO, RoleCreateDTO, RoleUpdateDTO>();
            ConfigureMappings<RoleAggregate, RoleDTO, RoleCreateDTO, RoleUpdateDTO>();
        }

        private void ConfigureMappings<TSource, TDTO, TCreateDTO, TUpdateDTO>()
        {
            CreateMap<TSource, TDTO>().ReverseMap();
            CreateMap<TSource, TCreateDTO>().ReverseMap();
            CreateMap<TSource, TUpdateDTO>().ReverseMap();
        }
    }
}