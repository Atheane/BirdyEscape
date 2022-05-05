using System;
using Libs.Domain.Entities;
using Domain.DomainEvents;
using Domain.ValueObjects;
using Domain.Types;
using Domain.Exceptions;


namespace Domain.Entities
{
    public interface ITileEntity : IAggregateRoot
    {
        public Guid _id { get; }
        public VOCoordinates _coordinates { get; }
        public IArrowEntity _arrow { get; }
        public void AddArrow(EnumDirection direction, VOCoordinates coordinates, VOPath path, bool effectOnce);
        public void UpdateArrowDirection(EnumDirection direction);
        public void UpdateArrowEffect(int numberEffects);
        public void RemoveArrow();
        public void Delete();
    }

    public class TileEntity : AggregateRoot, ITileEntity
    {
        public Guid _id { get; private set; }
        public VOCoordinates _coordinates { get; private set; }
        public IArrowEntity _arrow { get; private set; }


        private TileEntity(VOCoordinates coords) : base()
        {
            _coordinates = coords;
        }

        public static TileEntity Create(VOCoordinates coords)
        {
            var tile = new TileEntity(coords);
            var tileCreated = new TileCreated(tile);
            tile.AddDomainEvent(tileCreated);
            tile._id = tileCreated._id;
            return tile;
        }

        public void AddArrow(EnumDirection direction, VOCoordinates coordinates, VOPath path, bool effectOnce)
        {
            var arrowEntity = ArrowEntity.Create(direction, coordinates, path, effectOnce);
            var arrowCreated = new TileArrowAdded(this);
            AddDomainEvent(arrowCreated);
            arrowEntity._id = arrowCreated._id;
            _arrow = arrowEntity;
        }

        public void UpdateArrowEffect(int numberEffects)
        {
            IArrowEntity arrowUpdated = _arrow.UpdateEffect(numberEffects);
            _arrow = arrowUpdated;
            var arrowEffectUpdated = new TileArrowEffectUpdated(this);
            AddDomainEvent(arrowEffectUpdated);
        }

        public void UpdateArrowDirection(EnumDirection direction)
        {
            var arrowUpdated = _arrow.UpdateDirection(direction);
            _arrow = arrowUpdated;
            var arrowDirectionUpdated = new TileArrowDirectionUpdated(this);
            AddDomainEvent(arrowDirectionUpdated);
        }

        public void RemoveArrow()
        {
            if (_arrow == null)
            {
                throw new TileException.MissingArrow("This tile has no arrow, cannot delete it");
            }
            _arrow = null;
            var tileArrowRemoved = new TileArrowRemoved(this);
            AddDomainEvent(tileArrowRemoved);
        }

        public void Delete()
        {
            var tileDelete = new TileDeleted(this);
            AddDomainEvent(tileDelete);
        }
    }
}

