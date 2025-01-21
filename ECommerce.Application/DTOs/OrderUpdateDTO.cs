﻿using ECommerce.Domain.Enums;
using ECommerce.Domain.ValueObjects;

namespace ECommerce.Application.DTOs
{
    public class OrderUpdateDTO
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public eOrderStatus OrderStatus { get; set; }

        public Money TotalAmount { get; set; }

        public string PaymentMethod { get; set; }

        public Guid AddressId { get; set; }
    }
}