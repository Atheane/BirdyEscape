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
        public bool _effectOnce { get; }
        public IArrowEntity UpdateDirection(EnumDirection direction);
    }

    public class ArrowEntity : AggregateRoot, IArrowEntity
    {
        public Guid _id { get; set; }
        public EnumDirection _direction { get; set; }
        public VOCoordinates _coordinates { get; private set; }
        public VOPath _path { get; private set; }
        public bool _effectOnce { get; }

        private ArrowEntity(EnumDirection direction, VOCoordinates coords, VOPath path, bool effectOnce) : base()
        {
            _direction = direction;
            _coordinates = coords;
            _path = path;
            _effectOnce = effectOnce;
        }

        public static ArrowEntity Create(EnumDirection direction, VOCoordinates coords, VOPath path, bool effectOnce)
        {
            var arrow = new ArrowEntity(direction, coords, path, effectOnce);
            return arrow;
        }

        public IArrowEntity UpdateDirection(EnumDirection direction)
        {
            _direction = direction;
            return this;
        }

    }
}

