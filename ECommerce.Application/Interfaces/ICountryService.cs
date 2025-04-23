using ECommerce.Application.DTOs;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface ICountryService
    {
        Task<List<CountryDTO>> GetAllCountriesAsync(RequestParams? requestParams = null, bool useQuery = false);

        Task<CountryDTO> GetCountryByIdAsync(Guid id, bool useQuery = false);

        Task CreateCountryAsync(CountryCreateDTO dto);

        Task UpdateCountryAsync(CountryUpdateDTO dto);

        Task DeleteCountryAsync(Guid id);
    }
}