using UniMediator;
using System.Threading.Tasks;

namespace Adapters.Unimediatr
{
    public sealed class DomainEventNotification<IDomainEvent> : ISingleMessage<Task>
    {
        public IDomainEvent _domainEvent { get; }
        public DomainEventNotification(IDomainEvent domainEvent)
        {
            _domainEvent = domainEvent;
        }
    }
}