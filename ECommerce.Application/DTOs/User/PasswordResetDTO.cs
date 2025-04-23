namespace ECommerce.Application.DTOs.User
{
    public class PasswordResetDTO
    {
        public string Token { get; set; }

        public string NewPassword { get; set; }
    }
}