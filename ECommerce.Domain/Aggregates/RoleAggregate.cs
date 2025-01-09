using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;
using MediatR;

namespace ECommerce.Domain.Aggregates
{
    public class RoleAggregate : AggregateRoot<Role>
    {
        private readonly IMediator _mediator;
        public Role Role { get; set; }

        public RoleAggregate(Role entity, IMediator mediator)
            : base(entity, mediator)
        {
            Role = entity;
            _mediator = mediator;
        }

        public async Task CreateRole(Role role)
        {
            Role.CreateRole(role.Name);

            EventType = eEventType.RoleCreated;
            await RaiseDomainEvent();
        }

        public async Task UpdateRole(Role role)
        {
            Role.UpdateRole(role.Id, role.Name);

            EventType = eEventType.RoleUpdated;
            await RaiseDomainEvent();
        }

        public async Task DeleteRole()
        {
            EventType = eEventType.RoleDeleted;
            await RaiseDomainEvent();
        }

        private async Task RaiseDomainEvent()
        {
            var domainEvent = new RoleEvent(Role.Id, Role.Name, EventType);
            await RaiseDomainEvent(domainEvent);
        }

        public new void ClearDomainEvents()
        {
            base.ClearDomainEvents();
        }
    }
}