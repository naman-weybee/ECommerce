using ECommerce.Application.DTOs;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Interfaces
{
    public interface IAddressService
    {
        Task<List<AddressDTO>> GetAllAddressesAsync(RequestParams requestParams);

        Task<AddressDTO> GetAddressByIdAsync(Guid id);

        Task CreateAddressAsync(AddressCreateDTO dto);

        Task UpdateAddressAsync(AddressUpdateDTO dto);

        Task DeleteAddressAsync(Guid id);
    }
}