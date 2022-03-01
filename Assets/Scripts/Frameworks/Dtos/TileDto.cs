using System;
using UnityEngine;
using Domain.ValueObjects;

namespace Frameworks.Dtos
{
    public interface ITileDto
    {
        public Guid _id { get; }
        public Vector3 _position { get; }
        public string _path { get; }
        public IArrowDto _arrow { get; }
        public void AddArrow(IArrowDto arrowDto);
    }

    public class TileDto : ITileDto
    {
        public Guid _id { get; private set; }
        public Vector3 _position { get; private set; }
        public string _path { get; private set; }
        public IArrowDto _arrow { get; private set; }

        private TileDto(Guid id, Vector3 position, string path)
        {
            _id = id;
            _position = position;
            _path = path;
            _arrow = null;
        }

        public static TileDto Create(Guid id, VOCoordinates coordinates, VOPath path)
        {
            var position = new Vector3(
                coordinates.Value.X,
                PuzzleController.MIN.y,
                coordinates.Value.Y
            );
            return new TileDto(id, position, path.Value);
        }

        public void AddArrow(IArrowDto arrowDto)
        {
            _arrow = arrowDto;
        }
    }
}
