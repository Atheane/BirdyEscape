using System;
using Libs.Domain.Entities;
using Domain.DomainEvents;
using Domain.ValueObjects;
using Domain.Types;
using Domain.Exceptions;


namespace Domain.Entities
{
    public interface IArrowEntity : IAggregateRoot
    {
        public Guid _id { get; }
        public EnumDirection _direction { get; }
        public VOCoordinates _coordinates { get; }
        public VOPath _path { get; }
        public void UpdateDirection(EnumDirection direction);
        public void Delete();
    }

    public class ArrowEntity : AggregateRoot, IArrowEntity
    {
        public Guid _id { get; private set; }
        public EnumDirection _direction { get; private set; }
        public VOCoordinates _coordinates { get; private set; }
        public VOPath _path { get; private set; }

        private ArrowEntity(EnumDirection direction, VOCoordinates coords, VOPath path) : base()
        {
            _direction = direction;
            _coordinates = coords;
            _path = path;
        }

        public static ArrowEntity Create(EnumDirection direction, VOCoordinates coords, VOPath path)
        {
            var arrow = new ArrowEntity(direction, coords, path);
            var arrowCreated = new ArrowCreatedDomainEvent(arrow);
            arrow.AddDomainEvent(arrowCreated);
            arrow._id = arrowCreated._id;
            return arrow;
        }

        public void UpdateDirection(EnumDirection direction)
        {
            var arrowDirectionUpdated = new ArrowDirectionUpdatedDomainEvent(this);
            _direction = direction;
            AddDomainEvent(arrowDirectionUpdated);
        }

        public void Delete()
        {
            var arrowDeleted = new ArrowDeletedDomainEvent(this);
            AddDomainEvent(arrowDeleted);
        }

    }
}

