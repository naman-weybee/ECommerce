using ECommerce.Application.DTOs;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface IGenderService
    {
        Task<List<GenderDTO>> GetAllGendersAsync(RequestParams? requestParams = null);

        Task<GenderDTO> GetGenderByIdAsync(Guid id);

        Task CreateGenderAsync(GenderCreateDTO dto);

        Task UpdateGenderAsync(GenderUpdateDTO dto);

        Task DeleteGenderAsync(Guid id);
    }
}