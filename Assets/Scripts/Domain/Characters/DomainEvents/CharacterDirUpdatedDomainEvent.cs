using System;
using Libs.Domain.DomainEvents;
using Domain.Characters.Entities;

namespace Domain.Characters.DomainEvents
{
    public class CharacterDirUpdatedDomainEvent: DomainEvent
    {
        public new EnumCharacterEvents _label = EnumCharacterEvents.CHARACTER_DIRECTION_UPDATED;
        public new Guid _id = Guid.NewGuid();
        public new DateTime _createdAtUtc = DateTime.UtcNow;
        public ICharacterEntity _props;

        public CharacterDirUpdatedDomainEvent(ICharacterEntity props)
        {
            _props = props;
        }
    }
}
