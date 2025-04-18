﻿using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Entities
{
    public class RoleEntity
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
    }
}