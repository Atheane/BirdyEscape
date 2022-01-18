using System;
using Libs.Domain.Entities;
using Domain.Characters.DomainEvents;
using Domain.Characters.ValueObjects;
using Domain.Characters.Types;

namespace Domain.Characters.Entities
{
    public interface ICharacterEntity
    {
        public Guid Id { get; }
        public EnumCharacter Type { get; }
        public EnumDirection Direction { get; }
        public VOPosition Position { get; }
        public float Speed { get; }
    }

    public class CharacterEntity : AggregateRoot, ICharacterEntity
    {
        public new Guid Id { get; private set; }
        public EnumCharacter Type { get; private set; }
        public EnumDirection Direction { get; private set; }
        public VOPosition Position { get; private set; }
        public float Speed { get;  private set; }

        private CharacterEntity(EnumCharacter type, EnumDirection direction, VOPosition position, float speed) : base()
        {
            this.Type = type;
            this.Direction = direction;
            this.Position = position;
            this.Speed = speed;

        }

        public static CharacterEntity Create(EnumCharacter type, EnumDirection direction, VOPosition position, float speed)
        {
            var character = new CharacterEntity(type, direction, position, speed);
            var characterCreated = new CharacterCreatedDomainEvent<ICharacterEntity>(character);
            character.AddDomainEvent(characterCreated);
            character.Id = characterCreated.Id;
            return character;
        }

        public void MoveOnce()
        {
            (double X, double Y) position = this.Position.Value;
            switch (this.Direction)
            {
                case EnumDirection.LEFT:
                    position.X -= 1;
                    break;
                case EnumDirection.UP:
                    position.Y -= 1;
                    break;
                case EnumDirection.RIGHT:
                    position.X += 1;
                    break;
                case EnumDirection.DOWN:
                    position.X += 1;
                    break;
            }

            this.Position = VOPosition.Create(position);
        }
        public void MoveAlways()
        {
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
