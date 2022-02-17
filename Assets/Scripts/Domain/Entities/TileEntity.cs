using System;
using Libs.Domain.Entities;
using Domain.DomainEvents;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public interface ITileEntity : IAggregateRoot
    {
        public Guid _id { get; }
        public VOCoordinates _coordinates { get; }
        public VOPath _path { get; }
    }

    public class TileEntity : AggregateRoot, ITileEntity
    {
        public Guid _id { get; private set; }
        public VOCoordinates _coordinates { get; private set; }
        public VOPath _path { get; private set; }


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

    }
}

