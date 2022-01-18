using System;
using Libs.Domain.DomainEvents;


namespace Domain.Characters.DomainEvents
{
    public enum EnumCharacterEvents
    {
        CHARACTER_CREATED
    }
    public class CharacterCreatedDomainEvent<T> : DomainEvent
    {
        public new EnumCharacterEvents Label = EnumCharacterEvents.CHARACTER_CREATED;
        public new Guid Id = Guid.NewGuid();
        public new DateTime CreatedAtUtc = DateTime.UtcNow;
        public T Props;

        public CharacterCreatedDomainEvent(T props)
        {
            this.Props = props;
        }
    }
}
