using ECommerce.Application.DTOs.Address;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface IAddressService
    {
        Task<List<AddressDTO>> GetAllAddressesAsync(RequestParams? requestParams = null);

        Task<List<AddressDTO>> GetAllAddressesByUserAsync(Guid userId, RequestParams? requestParams = null);

        Task<AddressDTO> GetAddressByIdAsync(Guid id);

        Task<AddressDTO> GetSpecificAddressByUserAsync(Guid id, Guid userId);

        Task UpsertAddressAsync(AddressUpsertDTO dto);

        Task UpdateAddressTypeAsync(AddressTypeUpdateDTO dto);

        Task DeleteAddressByUserAsync(Guid id, Guid userId);

        Task DeleteAddressAsync(Guid id);
    }
}