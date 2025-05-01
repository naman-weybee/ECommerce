using ECommerce.Application.DTOs.State;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface IStateService
    {
        Task<List<StateDTO>> GetAllStatesAsync(RequestParams? requestParams = null, bool useQuery = false);

        Task<List<StateDTO>> GetAllStatesByCountryIdAsync(Guid countryId, RequestParams? requestParams = null, bool useQuery = false);

        Task<StateDTO> GetStateByIdAsync(Guid id, bool useQuery = false);

        Task UpsertStateAsync(StateUpsertDTO dto);

        Task DeleteStateAsync(Guid id);
    }
}