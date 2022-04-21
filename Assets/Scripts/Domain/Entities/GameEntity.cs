using System;
using Libs.Domain.Entities;
using Domain.DomainEvents;
using Domain.ValueObjects;
using Domain.Exceptions;
using UnityEngine;

namespace Domain.Entities
{
    public interface IGameEntity : IAggregateRoot
    {
        public Guid _id { get; }
        public int _currentLevelNumber { get; }
        public VOEnergy _energy { get; }
        public DateTime _firstConnectionDate { get; }
        public DateTime _lastConnectionDate { get; }
        public void UpdateLevel(int currentLevelNumber);
        public void ComputeEnergy(ILevelEntity currentLevel);
    }

    public class GameEntity : AggregateRoot, IGameEntity
    {
        public Guid _id { get; private set; }
        public int _currentLevelNumber { get; private set; }
        public VOEnergy _energy { get; private set; }
        public DateTime _firstConnectionDate { get; private set; }
        public DateTime _lastConnectionDate { get; private set; }


        private GameEntity(int currentLevelNumber, VOEnergy energy) : base()
        {
            _currentLevelNumber = currentLevelNumber;
            _energy = energy;
        }

        public static GameEntity Create(int currentLevelNumber)
        {
            var game = new GameEntity(currentLevelNumber, VOEnergy.Create());
            var gameCreated = new GameCreated(game);
            game.AddDomainEvent(gameCreated);
            game._id = gameCreated._id;
            game._firstConnectionDate = gameCreated._createdAtUtc;
            game._lastConnectionDate = gameCreated._createdAtUtc;
            return game;
        }

        public static GameEntity Load(Guid id, int currentLevelNumber, VOEnergy energy, DateTime firstConnectionDate)
        {
            var game = new GameEntity(currentLevelNumber, energy);
            game._id = id;
            game._firstConnectionDate = firstConnectionDate;

            var gameLoaded = new GameLoaded(game);
            game.AddDomainEvent(gameLoaded);
            return game;
        }

        public void UpdateLevel(int currentLevelNumber)
        {
            _currentLevelNumber = currentLevelNumber;
            var levelUpdated = new GameLevelUpdated(this);
            AddDomainEvent(levelUpdated);
        }

        public void ComputeEnergy(ILevelEntity currentLevel)
        {
            try
            {
                _energy = VOEnergy.Compute(_energy.Value, currentLevel._totalDistance, _lastConnectionDate);
                _lastConnectionDate = DateTime.UtcNow;
                var energyUpdated = new GameEnergyComputed(this);
                AddDomainEvent(energyUpdated);
            } catch(Exception e)
            {
                Debug.Log("COMPUTE ENERGY");
                Debug.Log(e);
                if (e.GetType() == typeof(EnergyException.ShouldNotBeNegative))
                {
                    var gameOver = new GameOver(this);
                    AddDomainEvent(gameOver);
                }
            }
        }

    }
}