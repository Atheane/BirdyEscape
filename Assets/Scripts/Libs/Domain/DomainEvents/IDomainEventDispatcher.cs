using Libs.Domain.Entities;

namespace Libs.Domain.DomainEvents
{
    public interface IDomainEventDispatcher
    {
        string Dispatch(IAggregateRoot aggregateRoot);
    }
}
