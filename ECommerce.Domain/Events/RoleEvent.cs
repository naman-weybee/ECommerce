using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Events
{
    public class RoleEvent : BaseEvent
    {
        public Guid RoleId { get; }

        public string Name { get; }

        public RoleEvent(Guid roleId, string name, eEventType eventType)
        {
            if (roleId == Guid.Empty)
                throw new ArgumentException("Id cannot be empty.", nameof(roleId));

            RoleId = roleId;
            Name = name;
            EventType = eventType;
        }
    }
}