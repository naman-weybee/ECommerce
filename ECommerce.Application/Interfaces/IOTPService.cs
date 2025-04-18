using ECommerce.Application.DTOs;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface IOTPService
    {
        Task<List<OTPDTO>> GetAllOTPAsync(RequestParams requestParams);

        Task<OTPDTO> GetOTPByIdAsync(Guid id);

        Task CreateOTPAsync(OTPCreateFromEmailDTO dto);

        Task UpdateOTPAsync(OTPUpdateDTO dto);

        Task<OTPTokenDTO> VerifyOTPAsync(OTPVerifyDTO dto);

        Task SetOTPIsUsedAsync(Guid otpId);

        Task DeleteOTPAsync(Guid id);
    }
}