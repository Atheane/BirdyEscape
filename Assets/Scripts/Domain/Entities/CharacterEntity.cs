using System;
using Libs.Domain.Entities;
using Domain.DomainEvents;
using Domain.ValueObjects;
using Domain.Types;
using UnityEngine;

namespace Domain.Entities
{
    public interface ICharacterEntity: IAggregateRoot
    {
        public Guid _id { get; }
        public EnumCharacterType _type { get; }
        public EnumDirection _direction { get; }
        public VOPosition _position { get; }
        public int _speed { get; }
        public EnumCharacterState _state { get; }
        public float _totalDistance { get; }
        public void MoveOnce();
        public void UpdateState(EnumCharacterState state);
        public void UpdateDirection(EnumDirection direction);
        public void Restart(VOPosition position, EnumDirection direction);
        public void Delete();
    }

    public class CharacterEntity : AggregateRoot, ICharacterEntity
    {
        public Guid _id { get; private set; }
        public EnumCharacterType _type { get; private set; }
        public EnumDirection _direction { get; private set; }
        public VOPosition _position { get; private set; }
        public int _speed { get;  private set; }
        public EnumCharacterState _state { get; private set; }
        public float _totalDistance { get; private set; }


        private CharacterEntity(EnumCharacterType type, EnumDirection direction, VOPosition position, int speed, float totalDistance) : base()
        {
            _type = type;
            _direction = direction;
            _position = position;
            _speed = speed;
            _state = EnumCharacterState.IDLE;
            _totalDistance = totalDistance;
        }

        public static CharacterEntity Create(EnumCharacterType type, EnumDirection direction, VOPosition position, int speed)
        {
            var character = new CharacterEntity(type, direction, position, speed, 0);
            var characterCreated = new CharacterCreated(character);
            character.AddDomainEvent(characterCreated);
            character._id = characterCreated._id;
            character._totalDistance = 0;
            return character;
        }

        public static CharacterEntity Load(Guid id, EnumCharacterType type, EnumDirection direction, VOPosition position, int speed)
        {
            var character = new CharacterEntity(type, direction, position, speed, 0);
            var characterLoaded = new CharacterLoaded(character);
            character.AddDomainEvent(characterLoaded);
            character._id = id;
            character._totalDistance = 0;
            return character;
        }

        public void MoveOnce()
        {
            try
            {
                (float X, float Y, float Z) position = _position.Value;
                switch (_direction)
                {
                    case EnumDirection.UP:
                        position.X -= 0.2f;
                        break;
                    case EnumDirection.DOWN:
                        position.X += 0.2f;
                        break;
                    case EnumDirection.LEFT:
                        position.Z -= 0.2f;
                        break;
                    case EnumDirection.RIGHT:
                        position.Z += 0.2f;
                        break;
                }
                _position = VOPosition.Create(position);
                _totalDistance += 0.2f;
                // not necessary, and low perf
                //var characterPositionUpdated = new CharacterPositionUpdatedDomainEvent(this);
                //AddDomainEvent(characterPositionUpdated);
            }
            catch (Exception e)
            {
                Debug.Log(e);
                // if VOPosition is not validated, we want character to turn right
                EnumDirection newDirection = EnumDirection.LEFT;
                switch (_direction)
                {
                    case EnumDirection.LEFT:
                        newDirection = EnumDirection.UP;
                        break;
                    case EnumDirection.UP:
                        newDirection = EnumDirection.RIGHT;
                        break;
                    case EnumDirection.RIGHT:
                        newDirection = EnumDirection.DOWN;
                        break;
                }
                UpdateDirection(newDirection);
            }
            // this method is called in a update loop,
            // avoid to dispatch an event each time, hard bugs if you do
            // alternative: do not use update loop and move only by signals
            // my guess is that it is far less performant than update loop
            // todo: benchmark performance
        }

        public void UpdateDirection(EnumDirection direction)
        {
            _direction = direction;
            var characterDirectionUpdated = new CharacterDirUpdated(this);
            AddDomainEvent(characterDirectionUpdated);
        }

        public void UpdateState(EnumCharacterState state)
        {
            _state = state;
            var characterStateUpdated = new CharacterStateUpdated(this);
            AddDomainEvent(characterStateUpdated);
        }

        public void Restart(VOPosition position, EnumDirection direction)
        {
            _state = EnumCharacterState.IDLE;
            _direction = direction;
            _position = position;
            _totalDistance = 0;
            var characterRestarted = new CharacterRestarted(this);
            AddDomainEvent(characterRestarted);
        }

        public void Delete()
        {
            var characterDeleted = new CharacterDeleted(this);
            AddDomainEvent(characterDeleted);
        }
    }
}
