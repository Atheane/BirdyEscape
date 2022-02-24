using System;
using Libs.Domain.DomainEvents;
using Domain.Entities;

namespace Domain.DomainEvents
{
    public class ArrowDirectionUpdatedDomainEvent : IDomainEvent
    {
        public string _label { get; }
        public Guid _id { get; }
        public DateTime _createdAtUtc { get; }
        public IArrowEntity _props { get; }

        public ArrowDirectionUpdatedDomainEvent(IArrowEntity props)
        {
            _label = "ARROW_DIRECTION_UPDATED";
            _id = Guid.NewGuid();
            _createdAtUtc = DateTime.UtcNow;
            _props = props;
        }
    }
}

