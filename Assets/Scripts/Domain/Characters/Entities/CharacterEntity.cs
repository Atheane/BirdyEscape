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
        public float Speed { get; }
        public EnumCharacterState State { get; }
    }

    public class CharacterEntity : AggregateRoot, ICharacterEntity
    {
        public new Guid Id { get; private set; }
        public EnumCharacterType Type { get; private set; }
        public EnumCharacterDirection Direction { get; private set; }
        public VOPosition Position { get; private set; }
        public float Speed { get;  private set; }
        public EnumCharacterState State { get; private set; }

        private CharacterEntity(EnumCharacterType type, EnumCharacterDirection direction, VOPosition position, float speed) : base()
        {
            this.Type = type;
            this.Direction = direction;
            this.Position = position;
            this.Speed = speed;
            this.State = EnumCharacterState.IDLE;
        }

        public static CharacterEntity Create(EnumCharacterType type, EnumCharacterDirection direction, VOPosition position, float speed)
        {
            var character = new CharacterEntity(type, direction, position, speed);
            var characterCreated = new CharacterCreatedDomainEvent<ICharacterEntity>(character);
            character.AddDomainEvent(characterCreated);
            character.Id = characterCreated.Id;
            return character;
        }

        public void MoveOnce()
        {
            (float X, float Y) position = this.Position.Value;
            switch (this.Direction)
            {
                case EnumCharacterDirection.LEFT:
                    position.X -= 1;
                    break;
                case EnumCharacterDirection.UP:
                    position.Y -= 1;
                    break;
                case EnumCharacterDirection.RIGHT:
                    position.X += 1;
                    break;
                case EnumCharacterDirection.DOWN:
                    position.X += 1;
                    break;
            }

            this.Position = VOPosition.Create(position);
        }
        public void MoveAlways()
        {
            this.State = EnumCharacterState.MOVING;
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = this.Speed;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

        }
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            MoveOnce();
        }

    }
}
