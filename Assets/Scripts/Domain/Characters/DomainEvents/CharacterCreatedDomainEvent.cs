using System;
using Libs.Domain.DomainEvents;


namespace Domain.Characters.DomainEvents
{
    public class CharacterCreatedDomainEvent : DomainEvent
    {
        public new EnumCharacterEvents Label = EnumCharacterEvents.CHARACTER_CREATED;
        public new Guid Id = Guid.NewGuid();
        public new DateTime CreatedAtUtc = DateTime.UtcNow;
        public string Props;

        public CharacterCreatedDomainEvent(string props)
        {
            this.Props = props;
        }
    }
}
