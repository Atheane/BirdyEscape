using System;

namespace Libs.Domain.DomainEvents
{
    public abstract class DomainEvent : IDomainEvent
    {
        public Guid Id { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public enum Label { get, set }
    }
}