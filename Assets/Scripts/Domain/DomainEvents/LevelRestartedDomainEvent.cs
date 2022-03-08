using System;
using Libs.Domain.DomainEvents;
using Domain.Entities;

namespace Domain.DomainEvents
{
    public class LevelRestartedDomainEvent : IDomainEvent
    {
        public string _label { get; }
        public Guid _id { get; }
        public DateTime _createdAtUtc { get; }
        public ILevelEntity _props { get; }

        public LevelRestartedDomainEvent(ILevelEntity props)
        {
            _label = "LEVEL_RESTARTED";
            _id = Guid.NewGuid();
            _createdAtUtc = DateTime.UtcNow;
            _props = props;
        }
    }
}
