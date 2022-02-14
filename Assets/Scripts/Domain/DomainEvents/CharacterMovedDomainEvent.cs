using System;
using Libs.Domain.DomainEvents;
using Domain.Entities;


namespace Domain.DomainEvents
{
    public class CharacterBouncedDomainEvent : IDomainEvent
    {
        public string _label { get; }
        public Guid _id { get; }
        public DateTime _createdAtUtc { get; }
        public ICharacterEntity _props { get; }

        public CharacterBouncedDomainEvent(ICharacterEntity props)
        {
            _label = "CHARACTER_BOUNCED";
            _id = Guid.NewGuid();
            _createdAtUtc = DateTime.UtcNow;
            _props = props;
        }
    }
}
