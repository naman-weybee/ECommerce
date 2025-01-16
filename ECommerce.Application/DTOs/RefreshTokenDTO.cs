namespace ECommerce.Application.DTOs
{
    public class RefreshTokenDTO
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Token { get; set; }

        public bool IsExpired { get; set; }

        public DateTime? RevokedDate { get; set; }

        public DateTime ExpiredDate { get; set; }
    }
}