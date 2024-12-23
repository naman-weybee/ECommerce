﻿namespace ECommerce.Domain.Entities
{
    public class Base
    {
        public DateTime CreatedDate { get; private set; } = DateTime.UtcNow;

        public DateTime UpdatedDate { get; private set; } = DateTime.UtcNow;

        public DateTime? DeletedDate { get; private set; }

        public bool IsDeleted { get; private set; }

        public void Status_Updated()
        {
            UpdatedDate = DateTime.UtcNow;
        }

        public void Status_Deleted()
        {
            IsDeleted = true;
            DeletedDate = DateTime.UtcNow;
        }
    }
}