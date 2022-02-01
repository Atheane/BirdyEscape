using UniMediator;
using System.Collections.Generic;
using Libs.Domain.DomainEvents;

namespace Adapters.Unimediatr
{
    public sealed class DomainEventNotification<IDomainEvent> : IMulticastMessage
    {
        public IDomainEvent _domainEvent { get; }
        public DomainEventNotification(IDomainEvent domainEvent)
        {
            _domainEvent = domainEvent;
        }
    }
}