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
            ConfigureMappings<Product, ProductDTO, ProductUpsertDTO>();
            ConfigureMappings<ProductAggregate, ProductDTO, ProductUpsertDTO>();

            ConfigureMappings<Category, CategoryDTO, CategoryUpsertDTO>();
            ConfigureMappings<CategoryAggregate, CategoryDTO, CategoryUpsertDTO>();

            ConfigureMappings<Order, OrderDTO, OrderCreateDTO, OrderUpdateDTO>();
            ConfigureMappings<OrderAggregate, OrderDTO, OrderCreateDTO, OrderUpdateDTO>();

            ConfigureMappings<OrderItem, OrderItemDTO, OrderItemUpsertDTO, OrderItemUpdateDTO>();
            ConfigureMappings<OrderItemAggregate, OrderItemDTO, OrderItemUpsertDTO, OrderItemUpdateDTO>();

            ConfigureMappings<User, UserDTO, UserUpsertDTO>();
            ConfigureMappings<UserAggregate, UserDTO, UserUpsertDTO>();

            ConfigureMappings<Address, AddressDTO, AddressUpsertDTO>();
            ConfigureMappings<AddressAggregate, AddressDTO, AddressUpsertDTO>();

            ConfigureMappings<Product, ProductDTO, ProductStockChangeDTO, ProductPriceChangeDTO>();

            ConfigureMappings<CartItem, CartItemDTO, CartItemUpsertDTO>();
            ConfigureMappings<CartItemAggregate, CartItemDTO, CartItemUpsertDTO>();

            ConfigureMappings<Gender, GenderDTO, GenderUpsertDTO>();
            ConfigureMappings<GenderAggregate, GenderDTO, GenderUpsertDTO>();

            ConfigureMappings<Role, RoleDTO, RoleUpsertDTO>();
            ConfigureMappings<RoleAggregate, RoleDTO, RoleUpsertDTO>();

            ConfigureMappings<RefreshToken, RefreshTokenDTO, RefreshTokenUpsertDTO>();
            ConfigureMappings<RefreshTokenAggregate, RefreshTokenDTO, RefreshTokenUpsertDTO>();

            ConfigureMappings<Country, CountryDTO, CountryUpsertDTO>();
            ConfigureMappings<CountryAggregate, CountryDTO, CountryUpsertDTO>();

            ConfigureMappings<State, StateDTO, StateUpsertDTO>();
            ConfigureMappings<StateAggregate, StateDTO, StateUpsertDTO>();

            ConfigureMappings<City, CityDTO, CityUpsertDTO>();
            ConfigureMappings<CityAggregate, CityDTO, CityUpsertDTO>();

            ConfigureMappings<OTP, OTPDTO, OTPCreateDTO, OTPUpdateDTO>();
            ConfigureMappings<OTPAggregate, OTPDTO, OTPCreateDTO, OTPUpdateDTO>();
        }

        private void ConfigureMappings<TSource, TDTO, TUpsertDTO>()
        {
            CreateMap<TSource, TDTO>().ReverseMap();
            CreateMap<TSource, TUpsertDTO>().ReverseMap();
        }

        private void ConfigureMappings<TSource, TDTO, TUpsertDTO, TUpdateDTO>()
        {
            CreateMap<TSource, TDTO>().ReverseMap();
            CreateMap<TSource, TUpsertDTO>().ReverseMap();
            CreateMap<TSource, TUpdateDTO>().ReverseMap();
        }
    }
}