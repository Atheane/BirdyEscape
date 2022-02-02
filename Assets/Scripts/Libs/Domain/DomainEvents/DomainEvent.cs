using System;

namespace Libs.Domain.DomainEvents
{
    public abstract class DomainEvent : IDomainEvent
    {
        public Guid _id { get; }
        public DateTime _createdAtUtc { get; }
        public enum _label { get }
    }
}