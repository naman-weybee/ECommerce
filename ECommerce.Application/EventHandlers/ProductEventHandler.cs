using ECommerce.Domain.Events;
using MediatR;

namespace ECommerce.Application.EventHandlers
{
    public class ProductEventHandler : INotificationHandler<ProductEvent>
    {
        private readonly ILogger<ProductEventHandler> _logger;

        public ProductEventHandler(ILogger<ProductEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(ProductEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("ProductEvent received: EventType = {EventType}, ProductId = {ProductId}, Name = {Name}, Price = {Price}, Stock = {Stock}",
                notification.ProductId,
                notification.Name,
                notification.Price,
                notification.Stock,
                notification.EventType);

            await NotifyExternalSystems(notification);

            await UpdateAnalyticsService(notification);
        }

        private async Task NotifyExternalSystems(ProductEvent notification)
        {
            await Task.Run(() =>
            {
                // Example logic for notifying external systems.
                // Notify inventory service about stock changes.
                // Notify payment service about product pricing.

                // Simulate external service call logic here.
            });
        }

        private async Task UpdateAnalyticsService(ProductEvent notification)
        {
            await Task.Run(() =>
            {
                // Example logic for updating analytics.
                // Record event data for analytics or reporting.
                // Capture total stock, revenue, or product trends.

                // Simulate analytics service logic here.
            });
        }
    }
}