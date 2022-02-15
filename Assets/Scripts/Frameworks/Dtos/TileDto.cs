using System;
using UnityEngine;

namespace Frameworks.Dtos
{
    public interface ITileDto
    {
        public Guid _id { get; }
        public Vector2 _coordinates { get; }
        public string _path { get; }
    }

    public class TileDto : ITileDto
    {
        public Guid _id { get; private set; }
        public Vector2 _coordinates { get; private set; }
        public string _path { get; private set; }

        private TileDto(Guid id, Vector2 coordinates, string path)
        {
            _id = id;
            _coordinates = coordinates;
            _path = path;
        }

        public static TileDto Create(Guid id, Vector2 coordinates, string path)
        {
            var tileDto = new TileDto(id, coordinates, path);
            return tileDto;
        }

    }
}
