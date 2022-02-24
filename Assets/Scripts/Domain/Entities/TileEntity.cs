using System;
using Libs.Domain.Entities;
using Domain.DomainEvents;
using Domain.ValueObjects;
using Domain.Types;

namespace Domain.Entities
{
    public interface ITileEntity : IAggregateRoot
    {
        public Guid _id { get; }
        public VOCoordinates _coordinates { get; }
        public VOPath _path { get; }
        public IArrowEntity _arrow { get; }
        public void AddArrow(IArrowEntity arrow);
        public void UpdateArrowDirection(EnumDirection direction);
        public void RemoveArrow();
    }

    public class TileEntity : AggregateRoot, ITileEntity
    {
        public Guid _id { get; private set; }
        public VOCoordinates _coordinates { get; private set; }
        public VOPath _path { get; private set; }
        public IArrowEntity _arrow { get; set; }


        private TileEntity(VOCoordinates coords, VOPath path) : base()
        {
            _coordinates = coords;
            _path = path;
        }

        public static TileEntity Create(VOCoordinates coords, VOPath path)
        {
            var tile = new TileEntity(coords, path);
            var tileCreated = new TileCreatedDomainEvent(tile);
            tile.AddDomainEvent(tileCreated);
            tile._id = tileCreated._id;
            return tile;
        }

        public void AddArrow(IArrowEntity arrowEntity)
        {
            _arrow = arrowEntity;
            var arrowAddedToTile = new ArrowAddedToTileDomainEvent(this);
            AddDomainEvent(arrowAddedToTile);
        }

        public void UpdateArrowDirection(EnumDirection direction)
        {
            _arrow.UpdateDirection(direction);
        }

        public void RemoveArrow()
        {
            _arrow.Delete();
            _arrow = null;
            var arrowRemoved = new ArrowRemoved(this);
            AddDomainEvent(arrowRemoved);
        }
    }
}

