using ECommerce.Application.DTOs;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface IStateService
    {
        Task<List<StateDTO>> GetAllStatesAsync(RequestParams requestParams);

        Task<StateDTO> GetStateByIdAsync(Guid id);

        Task CreateStateAsync(StateCreateDTO dto);

        Task UpdateStateAsync(StateUpdateDTO dto);

        Task DeleteStateAsync(Guid id);
    }
}
