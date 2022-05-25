using System;
using Libs.Domain.DomainEvents;
using Domain.Entities;

namespace Domain.DomainEvents
{
    public class TileArrowEffectUpdated : IDomainEvent
    {
        public string _label { get; }
        public Guid _id { get; }
        public DateTime _createdAtUtc { get; }
        public ITileEntity _props { get; }

        public TileArrowEffectUpdated(ITileEntity props)
        {
            _label = "TILE_ARROW_EFFECT_UPDATED";
            _id = Guid.NewGuid();
            _createdAtUtc = DateTime.UtcNow;
            _props = props;
        }
    }
}
