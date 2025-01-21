using ECommerce.Application.DTOs;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface IAddressService
    {
        Task<List<AddressDTO>> GetAllAddressesAsync(RequestParams requestParams, Guid userId);

        Task<AddressDTO> GetAddressByIdAsync(Guid id, Guid userId);

        Task CreateAddressAsync(AddressCreateDTO dto);

        Task UpdateAddressAsync(AddressUpdateDTO dto);

        Task UpdateAddressTypeAsync(AddressTypeUpdateDTO dto);

        Task DeleteAddressAsync(Guid id, Guid userId);
    }
}