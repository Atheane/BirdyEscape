using System;

namespace Libs.Domain.DomainEvents
{
    public interface IDomainEvent
    {
        Guid Id { get; }

        DateTime CreatedAtUtc { get; }

        string Label { get; }

    }
}