using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Events
{
    public class RoleEntityEvent : BaseEvent
    {
        public int RoleEntityId { get; }

        public string Name { get; }

        public RoleEntityEvent(int roleEntityId, string name, eEventType eventType)
        {
            RoleEntityId = roleEntityId;
            Name = name;
            EventType = eventType;
        }
    }
}