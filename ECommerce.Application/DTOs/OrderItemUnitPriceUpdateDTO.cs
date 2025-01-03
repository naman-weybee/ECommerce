﻿using ECommerce.Domain.ValueObjects;

namespace ECommerce.Application.DTOs
{
    public class OrderItemUnitPriceUpdateDTO
    {
        public Guid Id { get; set; }

        public Money UnitPrice { get; set; }
    }
}