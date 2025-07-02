﻿namespace ECommerce.Infrastructure.Data.Seeders.SeederEntities
{
    public class Base
    {
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? DeletedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}