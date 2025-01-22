using ECommerce.Application.DTOs;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface ICountryService
    {
        Task<List<CountryDTO>> GetAllCountriesAsync(RequestParams requestParams);

        Task<CountryDTO> GetCountryByIdAsync(Guid id);

        Task CreateCountryAsync(CountryCreateDTO dto);

        Task UpdateCountryAsync(CountryUpdateDTO dto);

        Task DeleteCountryAsync(Guid id);
    }
}