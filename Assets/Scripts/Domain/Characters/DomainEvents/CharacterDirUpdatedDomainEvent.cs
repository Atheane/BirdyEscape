using System;
using Libs.Domain.DomainEvents;
using Domain.Characters.Entities;

namespace Domain.Characters.DomainEvents
{
    public class CharacterDirUpdatedDomainEvent: IDomainEvent
    {
        public string _label { get; }
        public Guid _id { get; }
        public DateTime _createdAtUtc { get; }
        public ICharacterEntity _props { get; }

    public CharacterDirUpdatedDomainEvent(ICharacterEntity props)
        {
            _label = "CHARACTER_DIRECTION_UPDATED";
            _id = Guid.NewGuid();
            _createdAtUtc = DateTime.UtcNow;
            _props = props;
        }
    }
}
