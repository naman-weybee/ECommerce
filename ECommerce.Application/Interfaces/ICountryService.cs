using ECommerce.Application.DTOs.Country;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface ICountryService
    {
        Task<List<CountryDTO>> GetAllCountriesAsync(RequestParams? requestParams = null, bool useQuery = false);

        Task<CountryDTO> GetCountryByIdAsync(Guid id, bool useQuery = false);

        Task UpsertCountryAsync(CountryUpsertDTO dto);

        Task DeleteCountryAsync(Guid id);
    }
}