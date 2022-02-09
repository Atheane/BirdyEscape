using System;
using Libs.Domain.Entities;
using Domain.Characters.DomainEvents;
using Domain.Characters.ValueObjects;
using Domain.Characters.Types;
using Domain.Commons.Types;


namespace Domain.Characters.Entities
{
    public interface ICharacterEntity: IAggregateRoot
    {
        public new Guid Id { get; }
        public EnumCharacterType Type { get; }
        public EnumDirection Direction { get; }
        public VOPosition Position { get; }
        public VOPosition Orientation { get; }
        public int Speed { get; }
        public EnumCharacterState State { get; }
        public void MoveOnce();
        public void Orientate();
        public void UpdateState(EnumCharacterState state);
        public void UpdateDirection(EnumDirection direction);
    }

    public class CharacterEntity : AggregateRoot, ICharacterEntity
    {
        public new Guid Id { get; private set; }
        public EnumCharacterType Type { get; private set; }
        public EnumDirection Direction { get; private set; }
        public VOPosition Position { get; private set; }
        public VOPosition Orientation { get; private set; }
        public int Speed { get;  private set; }
        public EnumCharacterState State { get; private set; }

        private CharacterEntity(EnumCharacterType type, EnumDirection direction, VOPosition position, int speed) : base()
        {
            Type = type;
            Direction = direction;
            Position = position;
            Speed = speed;
            State = EnumCharacterState.IDLE;
        }

        public static CharacterEntity Create(EnumCharacterType type, EnumDirection direction, VOPosition position, int speed)
        {
            var character = new CharacterEntity(type, direction, position, speed);
            character.Orientate();
            var characterCreated = new CharacterCreatedDomainEvent(character);
            character.AddDomainEvent(characterCreated);
            character.Id = characterCreated._id;
            return character;
        }

        public void Orientate()
        {
            switch(Direction)
            {
                case EnumDirection.LEFT:
                    Orientation = VOPosition.Create((0, -90f, 0));
                    break;
                case EnumDirection.RIGHT:
                    Orientation = VOPosition.Create((0, 90f, 0));
                    break;
                case EnumDirection.DOWN:
                    Orientation = VOPosition.Create((0, 180f, 0));
                    break;
                case EnumDirection.UP:
                    Orientation = VOPosition.Create((0, 0, 0));
                    break;
            }
        }

        public void MoveOnce()
        {
            (float X, float Y, float Z) position = Position.Value;
            switch (Direction)
            {
                case EnumDirection.LEFT:
                    position.X -= 0.2f;
                    break;
                case EnumDirection.UP:
                    position.Z += 0.2f;
                    break;
                case EnumDirection.RIGHT:
                    position.X += 0.2f;
                    break;
                case EnumDirection.DOWN:
                    position.Z -= 0.2f;
                    break;
            }

            Position = VOPosition.Create(position);
            // this method is called in a update loop,
            // avoid to dispatch an event each time, hard bugs if you do
            // alternative: do not use update loop and move only by signals
            // my guess is that it is far less performant than update loop
            // todo: benchmark performance
        }

        public void UpdateDirection(EnumDirection direction)
        {
            Direction = direction;
            this.Orientate();
            var characterDirectionUpdated = new CharacterDirUpdatedDomainEvent(this);
            AddDomainEvent(characterDirectionUpdated);
        }

        public void UpdateState(EnumCharacterState state)
        {
            State = state;
            var characterStateUpdated = new CharacterStateUpdatedDomainEvent(this);
            AddDomainEvent(characterStateUpdated);
        }
    }
}
