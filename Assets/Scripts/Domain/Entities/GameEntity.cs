using System;
using Libs.Domain.Entities;
using Domain.DomainEvents;

namespace Domain.Entities
{
    public interface IGameEntity : IAggregateRoot
    {
        public Guid _id { get; }
        public int _currentLevel { get; }
        public float _energy { get; }
        public DateTime _firstConnectionDate { get; }
        void UpdateLevel(int currentLevel);
        public void UpdateEnergy(float energy);
    }

    public class GameEntity : AggregateRoot, IGameEntity
    {
        public Guid _id { get; private set; }
        public int _currentLevel { get; private set; }
        public float _energy { get; private set; }
        public DateTime _firstConnectionDate { get; private set; }


        private GameEntity(int currentLevel, float energy) : base()
        {
            _currentLevel = currentLevel;
            _energy = energy;
        }

        public static GameEntity Create(int currentLevel, float energy)
        {
            var game = new GameEntity(currentLevel, energy);
            var gameCreated = new GameCreated(game);
            game.AddDomainEvent(gameCreated);
            game._id = gameCreated._id;
            game._firstConnectionDate = gameCreated._createdAtUtc;
            return game;
        }

        public static GameEntity Load(Guid id, int currentLevel, float energy, DateTime firstConnectionDate)
        {
            var game = new GameEntity(currentLevel, energy);
            game._id = id;
            game._firstConnectionDate = firstConnectionDate;

            var gameLoaded = new GameLoaded(game);
            game.AddDomainEvent(gameLoaded);
            return game;
        }

        public void UpdateEnergy(float energy)
        {
            _energy = energy;
            var energyUpdated = new GameEnergyUpdated(this);
            this.AddDomainEvent(energyUpdated);
        }

        public void UpdateLevel(int currentLevel)
        {
            _currentLevel = currentLevel;
            var levelUpdated = new GameLevelUpdated(this);
            this.AddDomainEvent(levelUpdated);
        }

    }
}


