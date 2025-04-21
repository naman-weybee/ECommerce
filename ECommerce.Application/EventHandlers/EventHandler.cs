using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ECommerce.Application.EventHandlers
{
    public class EventHandler<TEvent> : INotificationHandler<TEvent>
        where TEvent : class, INotification
    {
        private readonly ILogger<EventHandler> _logger;
        private static readonly string _filePath = Path.Combine("D:", "EventLogs", $"{DateTime.UtcNow:yyyy_MM_dd}", $"EventLog_{DateTime.UtcNow:yyyy_MM_dd}.json");
        private static readonly SemaphoreSlim _fileLock = new(1, 1);

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };

        public EventHandler(ILogger<EventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(TEvent notification, CancellationToken cancellationToken)
        {
            var eventDetails = notification.GetType()
                .GetProperties()
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(notification, null) ?? "null");

            eventDetails["Timestamp"] = DateTime.UtcNow.ToString("o");

            await _fileLock.WaitAsync(cancellationToken);

            try
            {
                var eventLogs = new List<Dictionary<string, object>>();
                if (File.Exists(_filePath))
                {
                    using var stream = File.OpenRead(_filePath);
                    if (stream.Length > 0)
                        eventLogs = await JsonSerializer.DeserializeAsync<List<Dictionary<string, object>>>(stream, _jsonOptions, cancellationToken) ?? new();
                }

                eventLogs.Add(eventDetails);
                var evenLog = JsonSerializer.Serialize(eventLogs, _jsonOptions);

                _logger.LogInformation(evenLog);
                await SaveMessageToFile(evenLog, cancellationToken);
            }
            finally
            {
                _fileLock.Release();
            }

            var singleEventog = JsonSerializer.Serialize(eventDetails, _jsonOptions);
            await NotifyExternalSystems(singleEventog);
            await UpdateAnalyticsService(singleEventog);
        }

        private async Task SaveMessageToFile(string message, CancellationToken cancellationToken)
        {
            var directory = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            await File.WriteAllTextAsync(_filePath, message, cancellationToken);
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