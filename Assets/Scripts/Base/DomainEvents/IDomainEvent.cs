using System;

namespace Base.DomainEvents
{
    public interface IDomainEvent
    {
        Guid Id { get; }

        DateTime CreatedAtUtc { get; }
    }
}
