using System;
using UnityEngine;
using Domain.ValueObjects;
using Domain.Constants;

namespace Frameworks.Dtos
{
    public interface ITileDto
    {
        public Guid _id { get; }
        public Vector3 _position { get; }
        public string _path { get; }
    }

    public class TileDto : ITileDto
    {
        public Guid _id { get; private set; }
        public Vector3 _position { get; private set; }
        public string _path { get; private set; }

        private TileDto(Guid id, Vector3 position, string path)
        {
            _id = id;
            _position = position;
            _path = path;
        }

        public static TileDto Create(Guid id, VOCoordinates coordinates, VOPath path)
        {
            var position = new Vector3(
                coordinates.Value.X,
                Position.INIT_Y,
                coordinates.Value.Y
            );
            return new TileDto(id, position, path.Value);
        }

    }
}
