using System;
using Libs.Domain.DomainEvents;
using Domain.Entities;

namespace Domain.DomainEvents
{
    public class LevelStateUpdated : IDomainEvent
    {
        public string _label { get; }
        public Guid _id { get; }
        public DateTime _createdAtUtc { get; }
        public ILevelEntity _props { get; }

        public LevelStateUpdated(ILevelEntity props)
        {
            _label = "LEVEL_STATE_UPDATED";
            _id = Guid.NewGuid();
            _createdAtUtc = DateTime.UtcNow;
            _props = props;
        }
    }
}

