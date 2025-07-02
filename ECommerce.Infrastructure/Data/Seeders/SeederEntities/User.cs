﻿using System.ComponentModel.DataAnnotations;

namespace ECommerce.Infrastructure.Data.Seeders.SeederEntities
{
    public class User : Base
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public Guid RoleId { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Guid GenderId { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsEmailVerified { get; set; }

        public string? EmailVerificationToken { get; set; }

        public bool IsPhoneNumberVerified { get; set; }

        public bool IsSubscribedToNotifications { get; set; }
    }
}