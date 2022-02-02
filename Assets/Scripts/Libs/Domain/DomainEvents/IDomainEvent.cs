using System;

namespace Libs.Domain.DomainEvents
{
    public interface IDomainEvent
    {
        Guid _id { get; }
        DateTime _createdAtUtc { get; }
        enum _label { get }
    }
}
