using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.Address;
using ECommerce.Application.DTOs.CartItem;
using ECommerce.Application.DTOs.Category;
using ECommerce.Application.DTOs.City;
using ECommerce.Application.DTOs.Country;
using ECommerce.Application.DTOs.Gender;
using ECommerce.Application.DTOs.Order;
using ECommerce.Application.DTOs.OrderItem;
using ECommerce.Application.DTOs.OTP;
using ECommerce.Application.DTOs.Product;
using ECommerce.Application.DTOs.RefreshToken;
using ECommerce.Application.DTOs.Role;
using ECommerce.Application.DTOs.State;
using ECommerce.Application.DTOs.User;
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

            ConfigureMappings<RefreshToken, RefreshTokenDTO, RefreshTokenCreateDTO, RefreshTokenUpdateDTO>();
            ConfigureMappings<RefreshTokenAggregate, RefreshTokenDTO, RefreshTokenCreateDTO, RefreshTokenUpdateDTO>();

            ConfigureMappings<Country, CountryDTO, CountryCreateDTO, CountryUpdateDTO>();
            ConfigureMappings<CountryAggregate, CountryDTO, CountryCreateDTO, CountryUpdateDTO>();

            ConfigureMappings<State, StateDTO, StateCreateDTO, StateUpdateDTO>();
            ConfigureMappings<StateAggregate, StateDTO, StateCreateDTO, StateUpdateDTO>();

            ConfigureMappings<City, CityDTO, CityCreateDTO, CityUpdateDTO>();
            ConfigureMappings<CityAggregate, CityDTO, CityCreateDTO, CityUpdateDTO>();

            ConfigureMappings<OTP, OTPDTO, OTPCreateDTO, OTPUpdateDTO>();
            ConfigureMappings<OTPAggregate, OTPDTO, OTPCreateDTO, OTPUpdateDTO>();
        }

        private void ConfigureMappings<TSource, TDTO, TCreateDTO, TUpdateDTO>()
        {
            CreateMap<TSource, TDTO>().ReverseMap();
            CreateMap<TSource, TCreateDTO>().ReverseMap();
            CreateMap<TSource, TUpdateDTO>().ReverseMap();
        }
    }
}