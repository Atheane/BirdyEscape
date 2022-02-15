using System;
using Libs.Domain.Entities;
using Domain.DomainEvents;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public interface ITileEntity : IAggregateRoot
    {
        public Guid _id { get; }
        public VOPositionGrid _position { get; }
        public VOImage _image { get; }
    }

    public class TileEntity : AggregateRoot, ITileEntity
    {
        public Guid _id { get; private set; }
        public VOPositionGrid _position { get; private set; }
        public VOImage _image { get; private set; }


        private TileEntity(VOPositionGrid position, VOImage image) : base()
        {
            _position = position;
            _image = image;
        }

        public static TileEntity Create(VOPositionGrid position, VOImage image)
        {
            var tile = new TileEntity(position, image);
            var tileCreated = new TileCreatedDomainEvent(tile);
            tile.AddDomainEvent(tileCreated);
            tile._id = tileCreated._id;
            return tile;
        }

    }
}

