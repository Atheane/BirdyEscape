using System;

namespace Libs.Domain.DomainEvents
{
    public abstract class DomainEvent : IDomainEvent
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime CreatedAtUtc { get; } = DateTime.UtcNow;
        public string Label { get; }
    }
}