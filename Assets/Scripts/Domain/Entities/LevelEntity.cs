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
        public (VOPosition, EnumDirection)[] _charactersInit { get; }
        public ICharacterEntity[] _characters { get; }
        public ITileEntity[] _tiles { get; }
        public EnumLevelState _state { get; }
        public List<ITileEntity> Restart();
        public void Pause();
    }

    public class LevelEntity : AggregateRoot, ILevelEntity
    {
        public Guid _id { get; private set; }
        public int _number { get; private set; }
        public (VOPosition, EnumDirection)[] _charactersInit { get; private set; }
        public ICharacterEntity[] _characters { get; private set; }
        public ITileEntity[] _tiles { get; private set; }
        public EnumLevelState _state { get; private set; }
        public float _totalDistance { get; private set; }


        private LevelEntity(int number, ICharacterEntity[] characters, ITileEntity[] tiles, EnumLevelState state) : base()
        {
            _number = number;
            var charactersInit = new List<(VOPosition, EnumDirection)>(); 
            foreach (ICharacterEntity character in characters)
            {
                charactersInit.Add((character._position, character._direction));
            }
            _charactersInit = charactersInit.ToArray();
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
            return level;
        }

        public List<ITileEntity> Restart()
        {
            _state = EnumLevelState.ON;
            for (int c = 0; c < _characters.Length; c++)
            {
                _characters[c].Restart(_charactersInit[c].Item1, _charactersInit[c].Item2);
            }

            var updatedTiles = new List<ITileEntity>();
            foreach (ITileEntity tile in _tiles)
            {
                if (tile._arrow != null)
                {
                    tile.RemoveArrow();
                    updatedTiles.Add(tile);
                }
            }
            var levelRestarted = new LevelRestarted(this);
            AddDomainEvent(levelRestarted);
            return updatedTiles;
        }

        public void Pause()
        {
            _state = EnumLevelState.PENDING;
            foreach (ICharacterEntity character in _characters)
            {
                character.Delete();
            }
            foreach (ITileEntity tile in _tiles)
            {
                tile.Delete();
            }
            var levelPaused = new LevelPaused(this);
            AddDomainEvent(levelPaused);
        }
    }
}


