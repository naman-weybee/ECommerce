using ECommerce.Application.DTOs.City;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface ICityService
    {
        Task<List<CityDTO>> GetAllCitiesAsync(RequestParams? requestParams = null);

        Task<List<CityDTO>> GetAllCitiesByStateIdAsync(Guid stateId, RequestParams? requestParams = null);

        Task<CityDTO> GetCityByIdAsync(Guid id);

        Task UpsertCityAsync(CityUpsertDTO dto);

        Task DeleteCityAsync(Guid id);
    }
}