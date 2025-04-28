using ECommerce.Application.DTOs.OTP;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface IOTPService
    {
        Task<List<OTPDTO>> GetAllOTPAsync(RequestParams? requestParams = null);

        Task<OTPDTO> GetOTPByIdAsync(Guid id);

        Task CreateOTPAsync(OTPCreateFromEmailDTO dto);

        void UpdateOTP(OTPUpdateDTO dto);

        Task<OTPTokenDTO> VerifyOTPAsync(OTPVerifyDTO dto);

        Task SetOTPIsUsedAsync(Guid otpId);

        Task DeleteOTPAsync(Guid id);
    }
}