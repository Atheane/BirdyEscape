using System;
using Libs.Domain.Entities;
using Domain.ValueObjects;
using Domain.Types;


namespace Domain.Entities
{
    public interface IArrowEntity : IAggregateRoot
    {
        public Guid _id { get; }
        public EnumDirection _direction { get; }
        public VOCoordinates _coordinates { get; }
        public VOPath _path { get; }
        public IArrowEntity UpdateDirection(EnumDirection direction);
    }

    public class ArrowEntity : AggregateRoot, IArrowEntity
    {
        public Guid _id { get; set; }
        public EnumDirection _direction { get; set; }
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
            return arrow;
        }

        public IArrowEntity UpdateDirection(EnumDirection direction)
        {
            _direction = direction;
            return this;
        }

    }
}

