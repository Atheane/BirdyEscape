using System;
using Base.Entities;
using Characters.DomainEvents;
using Characters.Exceptions;
using Characters.ValueObjects;
using Characters.Types;

namespace Characters.Entities
{
    public class CharacterEntity : AggregateRoot
    {
        public VOImage Image { get; private set; }
        public EnumCharacter Type { get; private set; }
        public EnumDirection Direction { get; private set; }
        public VOPosition Position { get; private set; }
        private CharacterEntity() { }
        private CharacterEntity(Guid id, VOImage image, EnumCharacter type, EnumDirection direction, VOPosition position) : base(id)
        {
            this.Image = image;
            this.Type = type;
            this.Direction = direction;
            this.Image = image;
            this.Position = position;

        }

        public static CharacterEntity Create(Guid id, VOImage image, EnumCharacter type, EnumDirection direction, VOPosition position)
        {
            var character = new CharacterEntity(id, image, type, direction, position);
            character.AddDomainEvent(new CharacterCreatedDomainEvent(character.Id));
            return character;
        }

        public void Move(VOPosition position)
        {
            if (this.Position.Equals(position))
                throw new UpdatePositionException("Character position remains unchanged");

            this.Position = position;
        }

    }
}
