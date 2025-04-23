using ECommerce.Application.DTOs;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface ICityService
    {
        Task<List<CityDTO>> GetAllCitiesAsync(RequestParams? requestParams = null);

        Task<List<CityDTO>> GetAllCitiesByStateIdAsync(Guid stateId, RequestParams? requestParams = null);

        Task<CityDTO> GetCityByIdAsync(Guid id);

        Task CreateCityAsync(CityCreateDTO dto);

        Task UpdateCityAsync(CityUpdateDTO dto);

        Task DeleteCityAsync(Guid id);
    }
}