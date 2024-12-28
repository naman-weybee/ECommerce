﻿using AutoMapper;
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
        }

        private void ConfigureMappings<TSource, TDTO, TCreateDTO, TUpdateDTO>()
        {
            CreateMap<TSource, TDTO>().ReverseMap();
            CreateMap<TSource, TCreateDTO>().ReverseMap();
            CreateMap<TSource, TUpdateDTO>().ReverseMap();
        }
    }
}