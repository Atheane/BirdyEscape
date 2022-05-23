using Libs.Domain.DomainEvents;
using System.Collections.Generic;

namespace Libs.Domain.Entities
{
    public interface IAggregateRoot : IEntity
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }

        void AddDomainEvent(IDomainEvent domainEvent);

        void ClearDomainEvents();
    }
}
