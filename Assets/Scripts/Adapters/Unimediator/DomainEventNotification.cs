using UniMediator;
using Libs.Domain.DomainEvents;

namespace Adapters.Unimediatr
{
    public class DomainEventNotification : IMulticastMessage
    {
        public IDomainEvent _domainEvent { get; }
        public DomainEventNotification(IDomainEvent domainEvent)
        {
            _domainEvent = domainEvent;
        }
    }
}