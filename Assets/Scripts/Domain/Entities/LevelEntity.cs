using System;
using Libs.Domain.Entities;
using Domain.DomainEvents;
using Domain.Types;

namespace Domain.Entities
{
    public interface ILevelEntity : IAggregateRoot
    {
        public Guid _id { get; }
        public int _number { get;  }
        public ICharacterEntity[] _characters { get; }
        public EnumLevelState _state { get; }
        public void UpdateState();
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

        public void UpdateState()
        {
            //todo
        }

        public void Restart()
        {
            //todo
        }

        public void Over()
        {
            //todo
        }

    }
}


