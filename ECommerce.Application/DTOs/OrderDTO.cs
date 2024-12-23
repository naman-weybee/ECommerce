﻿using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.ValueObjects;

namespace ECommerce.Application.DTOs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class OrderDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Order date is required.")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Order status is required.")]
        [EnumDataType(typeof(eOrderStatus), ErrorMessage = "Invalid order status.")]
        public eOrderStatus OrderStatus { get; set; }

        [Required(ErrorMessage = "Total amount is required.")]
        public Money TotalAmount { get; set; }

        [Required(ErrorMessage = "Payment method is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Payment method should be between 3 and 50 characters.")]
        public string PaymentMethod { get; set; }

        [Required(ErrorMessage = "Shipping address is required.")]
        public Address ShippingAddress { get; set; }

        public ICollection<OrderItem> OrderItems { get; private set; }
    }
}