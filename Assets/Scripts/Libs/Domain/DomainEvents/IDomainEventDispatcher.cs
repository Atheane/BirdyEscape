using Libs.Domain.Entities;

namespace Libs.Domain.DomainEvents
{
    public interface IDomainEventDispatcher
    {
        void Dispatch(IAggregateRoot aggregateRoot);
    }
}
