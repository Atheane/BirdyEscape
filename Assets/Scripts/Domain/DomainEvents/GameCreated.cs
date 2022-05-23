using System;
using Libs.Domain.DomainEvents;
using Domain.Entities;

namespace Domain.DomainEvents
{
    public class GameCreated : IDomainEvent
    {
        public string _label { get; }
        public Guid _id { get; }
        public DateTime _createdAtUtc { get; }
        public IGameEntity _props { get; }

        public GameCreated(IGameEntity props)
        {
            _label = "GAME_CREATED";
            _id = Guid.NewGuid();
            _createdAtUtc = DateTime.UtcNow;
            _props = props;
        }
    }
}
