using System;
using System.Collections.Generic;
using System.Linq;
using Libs.Domain.Entities;
using Domain.DomainEvents;
using Domain.ValueObjects;
using UnityEngine;

namespace Domain.Entities
{
    public interface IGameEntity : IAggregateRoot
    {
        public Guid _id { get; }
        public ILevelEntity _currentLevel { get; }
        public VOEnergy _energy { get; }
        public List<DateTime> _connectionsDate { get; }
        public void UpdateCurrentLevel(ILevelEntity currentLevel);
        public void ComputeEnergy();
    }

    public class GameEntity : AggregateRoot, IGameEntity
    {
        public Guid _id { get; private set; }
        public ILevelEntity _currentLevel { get; private set;  }
        public VOEnergy _energy { get; private set; }
        public List<DateTime> _connectionsDate { get; private set; }


        private GameEntity(ILevelEntity currentLevel, VOEnergy energy) : base()
        {
            _currentLevel = currentLevel;
            _energy = energy;
            _connectionsDate = new List<DateTime>();
        }

        public static GameEntity Create(ILevelEntity currentLevel, VOEnergy energy, List<DateTime> connectionsDate)
        {
            var game = new GameEntity(currentLevel, energy);
            var gameCreated = new GameCreated(game);
            game.AddDomainEvent(gameCreated);
            game._id = gameCreated._id;
            game._connectionsDate = connectionsDate;
            game._connectionsDate.Add(gameCreated._createdAtUtc);
            return game;
        }

        public static GameEntity Load(string id, ILevelEntity currentLevel, VOEnergy energy, List<DateTime> connectionsDate)
        {
            var game = new GameEntity(currentLevel, energy);
            game._id = Guid.Parse(id);
            game._connectionsDate = connectionsDate;

            var gameLoaded = new GameLoaded(game);
            game.AddDomainEvent(gameLoaded);
            return game;
        }

        public void UpdateCurrentLevel(ILevelEntity currentLevel)
        {
            _currentLevel = currentLevel;
            var levelUpdated = new GameLevelUpdated(this);
            AddDomainEvent(levelUpdated);
        }

        public void ComputeEnergy()
        {
           var totalDistance = 0f;
            foreach (ICharacterEntity character in _currentLevel._characters)
            {
                totalDistance += character._totalDistance;
            }
            try
            {
                _energy = VOEnergy.Compute(_energy.Value, totalDistance, _connectionsDate.Last());
            } catch
            {
                _energy = VOEnergy.Load(0f);
                var gameOver = new GameOver(this);
                AddDomainEvent(gameOver);
            }
            _connectionsDate.Add(DateTime.UtcNow);
            var energyUpdated = new GameEnergyComputed(this);
            AddDomainEvent(energyUpdated);
        }

    }
}