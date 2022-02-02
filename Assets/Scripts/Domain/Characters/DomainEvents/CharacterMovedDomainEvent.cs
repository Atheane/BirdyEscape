using System;
using Libs.Domain.DomainEvents;
using Domain.Characters.Entities;


namespace Domain.Characters.DomainEvents
{
    public class CharacterMovedDomainEvent : IDomainEvent
    {
        public string _label { get; }
        public Guid _id { get; }
        public DateTime _createdAtUtc { get; }
        public ICharacterEntity _props { get; }

        public CharacterMovedDomainEvent(ICharacterEntity props)
        {
            _label = "CHARACTER_MOVED";
            _id = Guid.NewGuid();
            _createdAtUtc = DateTime.UtcNow;
            _props = props;
        }
    }
}
