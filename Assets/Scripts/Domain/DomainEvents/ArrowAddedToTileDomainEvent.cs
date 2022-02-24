using System;
using Libs.Domain.DomainEvents;
using Domain.Entities;

namespace Domain.DomainEvents
{
    public class ArrowAddedToTileDomainEvent : IDomainEvent
    {
        public string _label { get; }
        public Guid _id { get; }
        public DateTime _createdAtUtc { get; }
        public ITileEntity _props { get; }

        public ArrowAddedToTileDomainEvent(ITileEntity props)
        {
            _label = "ARROW_ADDED_TO_TILE";
            _id = Guid.NewGuid();
            _createdAtUtc = DateTime.UtcNow;
            _props = props;
        }
    }
}
