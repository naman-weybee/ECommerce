using ECommerce.Application.DTOs.OrderStatus;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities.HelperEntities;
using ECommerce.Shared.Repositories;
using ECommerce.Shared.RequestModel;

namespace ECommerce.Application.Services
{
    public class OrderStatusService : IOrderStatusService
    {
        private readonly IRepository<OrderStatus> _repository;

        public OrderStatusService(IRepository<OrderStatus> repository)
        {
            _repository = repository;
        }

        public async Task<List<OrderStatusDTO>> GetAllOrderStatusAsync(RequestParams requestParams)
        {
            var items = await _repository.GetAllAsync(requestParams);

            return items?.Select(x => new OrderStatusDTO
            {
                StatusId = x.StatusId,
                Name = x.Name,
            })?.ToList()!;
        }
    }
}