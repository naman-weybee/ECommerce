﻿using System.ComponentModel.DataAnnotations;

namespace ECommerce.Infrastructure.Data.Seeders.SeederEntities
{
    public class Gender
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
    }
}