using ECommerce.Application.DTOs;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface IAddressService
    {
        Task<List<AddressDTO>> GetAllAddressesAsync(RequestParams? requestParams = null);

        Task<List<AddressDTO>> GetAllAddressesByUserAsync(Guid userId, RequestParams? requestParams = null);

        Task<AddressDTO> GetAddressByIdAsync(Guid id);

        Task<AddressDTO> GetSpecificAddressByUserAsync(Guid id, Guid userId);

        Task CreateAddressAsync(AddressCreateDTO dto);

        Task UpdateAddressAsync(AddressUpdateDTO dto);

        Task UpdateAddressTypeAsync(AddressTypeUpdateDTO dto);

        Task DeleteAddressAsync(Guid id, Guid userId);
    }
}