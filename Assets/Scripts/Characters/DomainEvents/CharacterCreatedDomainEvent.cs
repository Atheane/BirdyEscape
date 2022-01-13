using System;
using Base.DomainEvents;

namespace Characters.DomainEvents
{
    public sealed class CharacterCreatedDomainEvent : DomainEvent
    {
        public Guid CharacterId { get; }

        public CharacterCreatedDomainEvent(Guid characterId)
        {
            this.CharacterId = characterId;
        }
    }
}
