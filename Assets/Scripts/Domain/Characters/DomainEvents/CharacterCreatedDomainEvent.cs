using System;
using Libs.Domain.DomainEvents;

namespace Domain.Characters.DomainEvents
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
