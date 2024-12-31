using ECommerce.Domain.Events;
using MediatR;

namespace ECommerce.Application.EventHandlers
{
    public class OrderEventHandler : INotificationHandler<OrderEvent>
    {
        private readonly ILogger<OrderEventHandler> _logger;

        public OrderEventHandler(ILogger<OrderEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(OrderEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("OrderEvent received: EventType = {EventType}, OrderId = {OrderId}, UserId = {UserId}, TotalAmount = {TotalAmount}",
                notification.OrderId,
                notification.UserId,
                notification.TotalAmount,
                notification.EventType);

            await NotifyExternalSystems(notification);

            await UpdateAnalyticsService(notification);
        }

        private async Task NotifyExternalSystems(OrderEvent notification)
        {
            await Task.Run(() =>
            {
                // Example: Notify inventory service about the products in the order
                // Example: Notify payment service with the total amount and payment details

                // Simulate external service call logic here
                // e.g., Inventory reduction, payment processing
            });
        }

        private async Task UpdateAnalyticsService(OrderEvent notification)
        {
            await Task.Run(() =>
            {
                // Example: Update analytics or reporting service with the order data

                // Simulate analytics service logic here
                // e.g., Recording order count and total revenue
            });
        }
    }
}