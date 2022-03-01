using Libs.Adapters;
using Domain.ValueObjects;
using UnityEngine;
using System;

namespace Adapters.Mappers
{
    public class Vector3ToVOCoordinatesMapper : IMapper<VOCoordinates, Vector3>
    {
        public VOCoordinates ToDomain(Vector3 position)
        {
            var coordX = (int)Math.Floor(position[0] - PuzzleController.MIN.x);
            var coordY = (int)Math.Floor(position[2] - PuzzleController.MIN.z);
            return VOCoordinates.Create((coordX, coordY));
        }
    }
}

