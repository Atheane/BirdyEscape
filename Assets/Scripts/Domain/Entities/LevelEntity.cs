using System;
using System.Collections.Generic;
using Libs.Domain.Entities;
using Domain.DomainEvents;
using Domain.Types;
using Domain.ValueObjects;
using UnityEngine;

namespace Domain.Entities
{
    public interface ILevelEntity : IAggregateRoot
    {
        public Guid _id { get; }
        public int _number { get;  }
        public ICharacterEntity[] _characters { get; }
        public ITileEntity[] _tiles { get; }
        public EnumLevelState _state { get; }
        public float _totalDistance { get; }
        public void UpdateState(EnumLevelState state);
        public void Restart(
            (ICharacterEntity character, VOPosition position, EnumDirection direction, float totalDistance)[] arrayProps,
            ITileEntity[] tiles
        );
        public void Finish();
    }

    public class LevelEntity : AggregateRoot, ILevelEntity
    {
        public Guid _id { get; private set; }
        public int _number { get; private set; }
        public ICharacterEntity[] _characters { get; private set; }
        public ITileEntity[] _tiles { get; private set; }
        public EnumLevelState _state { get; private set; }
        public float _totalDistance { get; private set; }


        private LevelEntity(int number, ICharacterEntity[] characters, ITileEntity[] tiles, EnumLevelState state) : base()
        {
            _number = number;
            _characters = characters;
            _tiles = tiles;
            _state = state;
        }

        public static LevelEntity Create(int number, ICharacterEntity[] characters, ITileEntity[] tiles, EnumLevelState state)
        {
            var level = new LevelEntity(number, characters, tiles, state);
            var levelCreated = new LevelCreated(level);
            level.AddDomainEvent(levelCreated);
            level._id = levelCreated._id;
            level._totalDistance = 0;
            return level;
        }

        public void UpdateState(EnumLevelState state)
        {
            _state = state;
            var levelStateUpdated = new LevelStateUpdated(this);
            AddDomainEvent(levelStateUpdated);
        }

        public void Restart(
            (ICharacterEntity character, VOPosition position, EnumDirection direction, float totalDistance)[] arrayProps,
            ITileEntity[] tiles
        )
        {
            _state = EnumLevelState.OFF;

            List<ICharacterEntity> charactersEntities = new List<ICharacterEntity>();
            foreach ((ICharacterEntity character, VOPosition position, EnumDirection direction, float totalDistance) in arrayProps)
            {

                character.Restart(position, direction);
                charactersEntities.Add(character);
                _totalDistance += totalDistance;
            }

            List<ITileEntity> tilesEntities = new List<ITileEntity>();
            foreach (ITileEntity tile in tiles)
            {
                tile.RemoveArrow();
                tilesEntities.Add(tile);
            }
            var levelRestarted = new LevelRestarted(this);
            AddDomainEvent(levelRestarted);
            _characters = charactersEntities.ToArray();
            _tiles = tilesEntities.ToArray(); 
        }

        public void Finish()
        {
            _state = EnumLevelState.WIN;
            foreach (ICharacterEntity character in _characters)
            {
                _totalDistance += character._totalDistance;
                character.Delete();
            }
            foreach (ITileEntity tile in _tiles)
            {
                tile.Delete();
            }
        }
    }
}


