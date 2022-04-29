using System;
using Libs.Domain.DomainEvents;
using Domain.Entities;
using UnityEngine;

namespace Domain.DomainEvents
{
    public class GameEnergyComputed : IDomainEvent
    {
        public string _label { get; }
        public Guid _id { get; }
        public DateTime _createdAtUtc { get; }
        public IGameEntity _props { get; }

        public GameEnergyComputed(IGameEntity props)
        {
            _label = "GAME_ENERGY_COMPUTED";
            _id = Guid.NewGuid();
            _createdAtUtc = DateTime.UtcNow;
            _props = props;
        }
    }
}
