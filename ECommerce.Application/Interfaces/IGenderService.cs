using ECommerce.Application.DTOs.Gender;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface IGenderService
    {
        Task<List<GenderDTO>> GetAllGendersAsync(RequestParams? requestParams = null);

        Task<GenderDTO> GetGenderByIdAsync(Guid id);

        Task UpsertGenderAsync(GenderUpsertDTO dto);

        Task DeleteGenderAsync(Guid id);
    }
}