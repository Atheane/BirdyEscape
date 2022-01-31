using System;
using Libs.Domain.DomainEvents;

namespace Domain.Characters.DomainEvents
{
    public class CharacterDirectionUpdatedDomainEvent<T> : DomainEvent
    {
        public new EnumCharacterEvents Label = EnumCharacterEvents.CHARACTER_DIRECTION_UPDATED;
        public new Guid Id = Guid.NewGuid();
        public new DateTime CreatedAtUtc = DateTime.UtcNow;
        public T Props;

        public CharacterDirectionUpdatedDomainEvent(T props)
        {
            this.Props = props;
        }
    }
}


