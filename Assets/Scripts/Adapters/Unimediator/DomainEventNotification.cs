using UniMediator;

namespace Adapters.Unimediatr
{
    public class DomainEventNotification<IDomainEvent> : IMulticastMessage
    {
        public IDomainEvent _domainEvent { get; }
        public DomainEventNotification(IDomainEvent domainEvent)
        {
            _domainEvent = domainEvent;
        }
    }
}