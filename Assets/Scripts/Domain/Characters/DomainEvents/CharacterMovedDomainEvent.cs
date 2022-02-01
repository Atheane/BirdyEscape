using System;
using Libs.Domain.DomainEvents;


namespace Domain.Characters.DomainEvents
{
    public class CharacterMovedDomainEvent<T> : DomainEvent
    {
        public new EnumCharacterEvents Label = EnumCharacterEvents.CHARACTER_MOVED;
        public new Guid Id = Guid.NewGuid();
        public new DateTime CreatedAtUtc = DateTime.UtcNow;
        public T Props;

        public CharacterMovedDomainEvent(T props)
        {
            this.Props = props;
        }
    }
}
