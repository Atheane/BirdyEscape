using System;
using Libs.Domain.Entities;
using Domain.DomainEvents;
using Domain.ValueObjects;
using Domain.Types;


namespace Domain.Entities
{
    public interface IArrowEntity : IAggregateRoot
    {
        public new Guid Id { get; }
        public EnumDirection Direction { get; }
        public VOPositionGrid Position { get; }
    }

    public class ArrowEntity : AggregateRoot, IArrowEntity
    {
        public new Guid Id { get; private set; }
        public EnumDirection Direction { get; private set; }
        public VOPositionGrid Position { get; private set; }

        private ArrowEntity(EnumDirection direction, VOPositionGrid position) : base()
        {
            Direction = direction;
            Position = position;
        }

        public static ArrowEntity Create(EnumDirection direction, VOPositionGrid position)
        {
            var arrow = new ArrowEntity(direction, position);
            var arrowCreated = new ArrowCreatedDomainEvent(arrow);
            arrow.AddDomainEvent(arrowCreated);
            arrow.Id = arrowCreated._id;
            return arrow;
        }

    }
}

