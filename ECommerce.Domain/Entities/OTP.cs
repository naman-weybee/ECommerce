using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities
{
    public class OTP : Base
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Code { get; set; }

        public eOTPType Type { get; set; }

        public bool IsUsed { get; set; }

        public string? Token { get; set; }

        public DateTime OTPExpiredDate { get; set; }

        public DateTime? TokenExpiredDate { get; set; }

        public void CreateOTP(Guid userId, eOTPType otpType)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Code = new Random().Next(100000, 1000000).ToString("D6");
            Type = otpType;
            IsUsed = false;
            OTPExpiredDate = DateTime.UtcNow.AddMinutes(10);
        }

        public void UpdateOTP(Guid id, Guid userId, string code, eOTPType otpType, bool isUsed, string? token, DateTime otpExpiredDate, DateTime? tokenExpiredDate)
        {
            Id = id;
            UserId = userId;
            Code = code;
            Type = otpType;
            IsUsed = isUsed;
            Token = token;
            OTPExpiredDate = otpExpiredDate;
            TokenExpiredDate = tokenExpiredDate;

            StatusUpdated();
        }

        public void VerifyOTP()
        {
            Token = Guid.NewGuid().ToString();
            TokenExpiredDate = DateTime.UtcNow.AddMinutes(2);

            StatusUpdated();
        }

        public void MarkAsUsed()
        {
            IsUsed = true;

            StatusUpdated();
        }
    }
}