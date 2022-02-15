using System;
using Libs.Domain.Entities;
using Domain.DomainEvents;
using Domain.ValueObjects;
using Domain.Types;


namespace Domain.Entities
{
    public interface IArrowEntity : IAggregateRoot
    {
        public Guid _id { get; }
        public EnumDirection _direction { get; }
        public VOPositionGrid _position { get; }
    }

    public class ArrowEntity : AggregateRoot, IArrowEntity
    {
        public Guid _id { get; private set; }
        public EnumDirection _direction { get; private set; }
        public VOPositionGrid _position { get; private set; }

        private ArrowEntity(EnumDirection direction, VOPositionGrid position) : base()
        {
            _direction = direction;
            _position = position;
        }

        public static ArrowEntity Create(EnumDirection direction, VOPositionGrid position)
        {
            var arrow = new ArrowEntity(direction, position);
            var arrowCreated = new ArrowCreatedDomainEvent(arrow);
            arrow.AddDomainEvent(arrowCreated);
            arrow._id = arrowCreated._id;
            return arrow;
        }

    }
}

