namespace ECommerce.Application.DTOs
{
    public class PasswordResetDTO
    {
        public string Token { get; set; }

        public string NewPassword { get; set; }
    }
}