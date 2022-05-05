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
        public int _numberEffects { get; }
        public IArrowEntity UpdateDirection(EnumDirection direction);
        public IArrowEntity UpdateEffect(int numberEffects);
    }

    public class ArrowEntity : AggregateRoot, IArrowEntity
    {
        public Guid _id { get; set; }
        public EnumDirection _direction { get; set; }
        public VOCoordinates _coordinates { get; private set; }
        public VOPath _path { get; private set; }
        public bool _effectOnce { get; private set; }
        public int _numberEffects { get; private set; }

        private ArrowEntity(EnumDirection direction, VOCoordinates coords, VOPath path, bool effectOnce, int numberEffects) : base()
        {
            _direction = direction;
            _coordinates = coords;
            _path = path;
            _effectOnce = effectOnce;
            _numberEffects = numberEffects;
        }

        public static ArrowEntity Create(EnumDirection direction, VOCoordinates coords, VOPath path, bool effectOnce)
        {
            var arrow = new ArrowEntity(direction, coords, path, false, 0);
            return arrow;
        }

        public IArrowEntity UpdateDirection(EnumDirection direction)
        {
            _direction = direction;
            return this;
        }

        public IArrowEntity UpdateEffect(int numberEffects)
        {
            _effectOnce = true;
            _numberEffects = numberEffects;
            return this;
        }

    }
}

