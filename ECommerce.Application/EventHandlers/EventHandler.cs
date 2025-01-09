using MediatR;
using System.Text;

namespace ECommerce.Application.EventHandlers
{
    public class EventHandler<TEvent> : INotificationHandler<TEvent>
        where TEvent : class, INotification
    {
        private readonly ILogger<EventHandler> _logger;

        public EventHandler(ILogger<EventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(TEvent notification, CancellationToken cancellationToken)
        {
            var properties = notification.GetType().GetProperties();
            var message = new StringBuilder();

            message.AppendLine("------------------- Event Details ----------------------");

            foreach (var property in properties)
                message.AppendLine($"{property.Name}: {property.GetValue(notification, null)}");

            message.AppendLine($"Timestamp: {DateTime.UtcNow:dd-MM-yyyy HH:mm:ss}");
            message.AppendLine("--------------------------------------------------------");

            var notificationMessage = message?.ToString();
            _logger.LogInformation(notificationMessage);

            await SaveMessageToFile(notificationMessage!);
            await NotifyExternalSystems(notificationMessage!);
            await UpdateAnalyticsService(notificationMessage!);
        }

        private async Task SaveMessageToFile(string message)
        {
            var filePath = Path.Combine("D:", "EventLogs", $"EventLog_{DateTime.UtcNow:ddMMyyyy}.txt");

            var directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory!);

            await File.AppendAllTextAsync(filePath, message + Environment.NewLine);
        }

        private async Task NotifyExternalSystems(string notificationMessage)
        {
            await Task.Run(() =>
            {
                // Example logic for notifying external systems.
                // Notify related services about category updates or changes.

                // Simulate external service call logic here.
            });
        }

        private async Task UpdateAnalyticsService(string notificationMessage)
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