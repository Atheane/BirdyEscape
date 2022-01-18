using System;
using Libs.Domain.DomainEvents;
using Domain.Characters.Types;

public enum EnumCharacterEvents
{
    CHARACTER_CREATED
}

namespace Domain.Characters.DomainEvents
{
    public class CharacterCreatedDomainEvent : DomainEvent
    {
        public new EnumCharacterEvents Label = EnumCharacterEvents.CHARACTER_CREATED;
        public new Guid Id = Guid.NewGuid();
        public new DateTime CreatedAtUtc = DateTime.UtcNow;
        public EnumCharacter Props;

        public CharacterCreatedDomainEvent(EnumCharacter type)
        {
            Props = type;
        }
    }
}
