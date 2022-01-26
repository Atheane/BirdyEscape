using System;
using Libs.Domain.DomainEvents;


namespace Domain.Characters.DomainEvents
{
    public class CharacterStateUpdatedDomainEvent<T> : DomainEvent
    {
        public new EnumCharacterEvents Label = EnumCharacterEvents.CHARACTER_STATE_UPDATED;
        public new Guid Id = Guid.NewGuid();
        public new DateTime CreatedAtUtc = DateTime.UtcNow;
        public T Props;

        public CharacterStateUpdatedDomainEvent(T props)
        {
            this.Props = props;
        }
    }
}
