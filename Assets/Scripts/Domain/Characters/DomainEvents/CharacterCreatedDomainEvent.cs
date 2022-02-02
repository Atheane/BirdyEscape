using System;
using Libs.Domain.DomainEvents;
using Domain.Characters.Entities;

namespace Domain.Characters.DomainEvents
{
    public class CharacterCreatedDomainEvent : DomainEvent
    {
        public new EnumCharacterEvents _label = EnumCharacterEvents.CHARACTER_CREATED;
        public new Guid _id = Guid.NewGuid();
        public new DateTime _createdAtUtc = DateTime.UtcNow;
        public ICharacterEntity _props;

        public CharacterCreatedDomainEvent(ICharacterEntity props)
        {
            _props = props;
        }
    }
}
