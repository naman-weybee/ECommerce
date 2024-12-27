using ECommerce.Domain.Enums;
using MediatR;

namespace ECommerce.Domain.Events
{
    public class BaseEvent : INotification
    {
        public eEventType EventType { get; set; }
    }
}