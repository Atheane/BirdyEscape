using System;
using Libs.Domain.DomainEvents;
using Domain.Entities;

namespace Domain.DomainEvents
{
    public class GameLevelUpdated : IDomainEvent
    {
        public string _label { get; }
        public Guid _id { get; }
        public DateTime _createdAtUtc { get; }
        public IGameEntity _props { get; }

        public GameLevelUpdated(IGameEntity props)
        {
            _label = "GAME_LEVEL_UPDATED";
            _id = Guid.NewGuid();
            _createdAtUtc = DateTime.UtcNow;
            _props = props;
        }
    }
}
