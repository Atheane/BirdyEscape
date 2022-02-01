using System;
using Libs.Domain.Entities;
using Domain.Characters.DomainEvents;
using Domain.Characters.ValueObjects;
using Domain.Characters.Types;

namespace Domain.Characters.Entities
{
    public interface ICharacterEntity: IAggregateRoot
    {
        public new Guid Id { get; }
        public EnumCharacterType Type { get; }
        public EnumCharacterDirection Direction { get; }
        public VOPosition Position { get; }
        public int Speed { get; }
        public EnumCharacterState State { get; }
        public void MoveOnce();
        public void UpdateState(EnumCharacterState state);
        public void UpdateDirection(EnumCharacterDirection direction);
    }

    public class CharacterEntity : AggregateRoot, ICharacterEntity
    {
        public new Guid Id { get; private set; }
        public EnumCharacterType Type { get; private set; }
        public EnumCharacterDirection Direction { get; private set; }
        public VOPosition Position { get; private set; }
        public int Speed { get;  private set; }
        public EnumCharacterState State { get; private set; }

        private CharacterEntity(EnumCharacterType type, EnumCharacterDirection direction, VOPosition position, int speed) : base()
        {
            Type = type;
            Direction = direction;
            Position = position;
            Speed = speed;
            State = EnumCharacterState.IDLE;
        }

        public static CharacterEntity Create(EnumCharacterType type, EnumCharacterDirection direction, VOPosition position, int speed)
        {
            var character = new CharacterEntity(type, direction, position, speed);
            var characterCreated = new CharacterCreatedDomainEvent("test");
            character.AddDomainEvent(characterCreated);
            character.Id = characterCreated.Id;
            return character;
        }

        public void MoveOnce()
        {
            (float X, float Y) position = Position.Value;
            switch (Direction)
            {
                case EnumCharacterDirection.LEFT:
                    position.X -= 1;
                    break;
                case EnumCharacterDirection.UP:
                    position.Y += 1;
                    break;
                case EnumCharacterDirection.RIGHT:
                    position.X += 1;
                    break;
                case EnumCharacterDirection.DOWN:
                    position.Y -= 1;
                    break;
            }

            Position = VOPosition.Create(position);
            var characterMoved = new CharacterMovedDomainEvent<VOPosition>(Position);
            AddDomainEvent(characterMoved);
        }

        public void UpdateDirection(EnumCharacterDirection direction)
        {
            Direction = direction;
            var characterDirectionUpdated = new CharacterDirUpdatedDomainEvent<EnumCharacterDirection>(Direction);
            AddDomainEvent(characterDirectionUpdated);
        }

        public void UpdateState(EnumCharacterState state)
        {
            State = state;
            var characterStateUpdated = new CharacterStateUpdatedDomainEvent<EnumCharacterState>(State);
            AddDomainEvent(characterStateUpdated);
        }
    }
}
