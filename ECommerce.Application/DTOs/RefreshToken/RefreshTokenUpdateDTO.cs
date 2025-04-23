namespace ECommerce.Application.DTOs.RefreshToken
{
    public class RefreshTokenUpdateDTO
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Token { get; set; }

        public DateTime? RevokedDate { get; set; }

        public DateTime ExpiredDate { get; set; }
    }
}