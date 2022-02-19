using UniMediator;

namespace Adapters.Unimediatr
{
    public class DomainEventNotification<IDomainEvent> : ISingleMessage<string>
    {
        public IDomainEvent _domainEvent { get; }
        public DomainEventNotification(IDomainEvent domainEvent)
        {
            _domainEvent = domainEvent;
        }
    }
}