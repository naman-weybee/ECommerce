using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Events
{
    public class RolePermissionEvent : BaseEvent
    {
        public Guid RoleId { get; }

        public eRoleEntity RoleEntityId { get; }

        public RolePermissionEvent(Guid roleId, eRoleEntity roleEntityId, eEventType eventType)
        {
            if (roleId == Guid.Empty)
                throw new ArgumentException("RoleId cannot be empty.", nameof(roleId));

            if (roleEntityId == eRoleEntity.Unknown)
                throw new ArgumentException("RoleEntityId cannot be Unknown.", nameof(roleEntityId));

            RoleId = roleId;
            RoleEntityId = roleEntityId;
            EventType = eventType;
        }
    }
}