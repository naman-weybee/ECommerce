using ECommerce.Domain.Events;
using MediatR;

namespace ECommerce.Application.EventHandlers
{
    public class CategoryEventHandler : INotificationHandler<CategoryEvent>
    {
        private readonly ILogger<CategoryEventHandler> _logger;

        public CategoryEventHandler(ILogger<CategoryEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(CategoryEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CategoryEvent received: CategoryId = {CategoryId}, Name = {Name}, ParentCategoryId = {ParentCategoryId}",
                notification.CategoryId,
                notification.Name,
                notification.ParentCategoryId);

            await NotifyExternalSystems(notification);

            await UpdateAnalyticsService(notification);
        }

        private async Task NotifyExternalSystems(CategoryEvent notification)
        {
            await Task.Run(() =>
            {
                // Example logic for notifying external systems.
                // Notify related services about category updates or changes.

                // Simulate external service call logic here.
            });
        }

        private async Task UpdateAnalyticsService(CategoryEvent notification)
        {
            await Task.Run(() =>
            {
                // Example logic for updating analytics.
                // Record event data for analytics or reporting.
                // Capture category usage trends or hierarchy changes.

                // Simulate analytics service logic here.
            });
        }
    }
}