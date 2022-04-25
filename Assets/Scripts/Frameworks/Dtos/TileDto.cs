using System;
using UnityEngine;
using Domain.ValueObjects;

namespace Frameworks.Dtos
{
    public class TileDto
    {
        public string _id;
        public Vector3 _position;
        public IArrowDto _arrow;

        private TileDto(Guid id, Vector3 position)
        {
            _id = id.ToString();
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
