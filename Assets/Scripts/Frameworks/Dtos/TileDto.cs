using System;
using UnityEngine;
using Domain.ValueObjects;

namespace Frameworks.Dtos
{
    public interface ITileDto
    {
        public Guid _id { get; }
        public Vector3 _position { get; }
        public IArrowDto _arrow { get; }
    }

    public class TileDto: ITileDto
    {
        public Guid _id { get; private set; }
        public Vector3 _position { get; private set; }
        public IArrowDto _arrow { get; private set; }

        private TileDto(Guid id, Vector3 position)
        {
            _id = id;
            _position = position;
            _arrow = null;
        }

        public static TileDto Create(Guid id, VOCoordinates coordinates)
        {
            var position = new Vector3(
                coordinates.Value.X,
                PuzzleController.MIN.y,
                coordinates.Value.Y
            );
            return new TileDto(id, position);
        }

        public void AddArrow(IArrowDto arrowDto)
        {
            _arrow = arrowDto;
        }
    }
}
