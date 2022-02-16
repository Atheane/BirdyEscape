using System;
using Libs.Domain.Entities;
using Domain.DomainEvents;
using Domain.ValueObjects;
using Domain.Types;


namespace Domain.Entities
{
    public interface ICharacterEntity: IAggregateRoot
    {
        public Guid _id { get; }
        public EnumCharacterType _type { get; }
        public EnumDirection _direction { get; }
        public VOPosition3D _position { get; }
        public VOPosition3D _orientation { get; }
        public int _speed { get; }
        public EnumCharacterState _state { get; }
        public void MoveOnce();
        public void Orientate();
        public void UpdateState(EnumCharacterState state);
        public void UpdateDirection(EnumDirection direction);
    }

    public class CharacterEntity : AggregateRoot, ICharacterEntity
    {
        public Guid _id { get; private set; }
        public EnumCharacterType _type { get; private set; }
        public EnumDirection _direction { get; private set; }
        public VOPosition3D _position { get; private set; }
        public VOPosition3D _orientation { get; private set; }
        public int _speed { get;  private set; }
        public EnumCharacterState _state { get; private set; }

        private CharacterEntity(EnumCharacterType type, EnumDirection direction, VOPosition3D position, int speed) : base()
        {
            _type = type;
            _direction = direction;
            _position = position;
            _speed = speed;
            _state = EnumCharacterState.IDLE;
        }

        public static CharacterEntity Create(EnumCharacterType type, EnumDirection direction, VOPosition3D position, int speed)
        {
            var character = new CharacterEntity(type, direction, position, speed);
            character.Orientate();
            var characterCreated = new CharacterCreatedDomainEvent(character);
            character.AddDomainEvent(characterCreated);
            character._id = characterCreated._id;
            return character;
        }

        public void Orientate()
        {
            switch(_direction)
            {
                case EnumDirection.UP:
                    _orientation = VOPosition3D.Create((0, -90f, 0));
                    break;
                case EnumDirection.DOWN:
                    _orientation = VOPosition3D.Create((0, 90f, 0));
                    break;
                case EnumDirection.LEFT:
                    _orientation = VOPosition3D.Create((0, 180f, 0));
                    break;
                case EnumDirection.RIGHT:
                    _orientation = VOPosition3D.Create((0, 0, 0));
                    break;
            }
        }

        public void MoveOnce()
        {
            (float X, float Y, float Z) position = _position.Value;
            switch (_direction)
            {
                case EnumDirection.LEFT:
                    position.Z -= 0.25f;
                    break;
                case EnumDirection.UP:
                    position.X -= 0.25f;
                    break;
                case EnumDirection.RIGHT:
                    position.Z += 0.25f;
                    break;
                case EnumDirection.DOWN:
                    position.X += 0.25f;
                    break;
            }

            _position = VOPosition3D.Create(position);
            // this method is called in a update loop,
            // avoid to dispatch an event each time, hard bugs if you do
            // alternative: do not use update loop and move only by signals
            // my guess is that it is far less performant than update loop
            // todo: benchmark performance
        }

        public void UpdateDirection(EnumDirection direction)
        {
            _direction = direction;
            Orientate();
            var characterDirectionUpdated = new CharacterDirUpdatedDomainEvent(this);
            AddDomainEvent(characterDirectionUpdated);
        }

        public void UpdateState(EnumCharacterState state)
        {
            _state = state;
            var characterStateUpdated = new CharacterStateUpdatedDomainEvent(this);
            AddDomainEvent(characterStateUpdated);
        }
    }
}
