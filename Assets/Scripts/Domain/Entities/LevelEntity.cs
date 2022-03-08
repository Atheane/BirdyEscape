using System;
using System.Collections.Generic;
using Libs.Domain.Entities;
using Domain.DomainEvents;
using Domain.Types;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public interface ILevelEntity : IAggregateRoot
    {
        public Guid _id { get; }
        public int _number { get;  }
        public ICharacterEntity[] _characters { get; }
        public EnumLevelState _state { get; }
        public void UpdateState(EnumLevelState state);
        public void Restart((ICharacterEntity character, VOPosition position, EnumDirection direction)[] arrayProps);
    }

    public class LevelEntity : AggregateRoot, ILevelEntity
    {
        public Guid _id { get; private set; }
        public int _number { get; private set; }
        public ICharacterEntity[] _characters { get; private set; }
        public EnumLevelState _state { get; private set; }


        private LevelEntity(int number, ICharacterEntity[] characters, EnumLevelState state) : base()
        {
            _number = number;
            _characters = characters;
            _state = state;
        }

        public static LevelEntity Create(int number, ICharacterEntity[] characters, EnumLevelState state)
        {
            var level = new LevelEntity(number, characters, state);
            var levelCreated = new LevelCreated(level);
            level.AddDomainEvent(levelCreated);
            level._id = levelCreated._id;
            return level;
        }

        public void UpdateState(EnumLevelState state)
        {
            _state = state;
            var levelStateUpdated = new LevelStateUpdatedDomainEvent(this);
            AddDomainEvent(levelStateUpdated);
        }

        public void Restart((ICharacterEntity character, VOPosition position, EnumDirection direction)[] arrayProps)
        {
            _state = EnumLevelState.OFF;
            List<ICharacterEntity> charactersEntity = new List<ICharacterEntity>();

            foreach ((ICharacterEntity character, VOPosition position, EnumDirection direction) in arrayProps)
            {
                character.Restart(position, direction);
                charactersEntity.Add(character);
            }
            var levelRestarted = new LevelRestartedDomainEvent(this);
            AddDomainEvent(levelRestarted);
            _characters = charactersEntity.ToArray();
        }

        public void Over()
        {
            //todo
        }

    }
}


