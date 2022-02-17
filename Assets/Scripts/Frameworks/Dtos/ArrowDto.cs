using System;
using UnityEngine;
using Domain.Types;
using Domain.ValueObjects;

namespace Frameworks.Dtos
{
    public interface IArrowDto
    {
        public Guid _id { get; }
        public Vector3 _orientation { get; }
        public Vector3 _position { get; }
        public string _path { get; }
    }

    public class ArrowDto : IArrowDto
    {
        public Guid _id { get; private set; }
        public Vector3 _orientation { get; private set; }
        public Vector3 _position { get; private set; }
        public string _path { get; }

        private ArrowDto(Guid id, Vector3 orientation, Vector3 position, string path)
        {
            _id = id;
            _orientation = orientation;
            _position = position;
            _path = path;
        }

        public static ArrowDto Create(Guid id, EnumDirection direction, VOCoordinates coordinates, VOPath path)
        {
            Vector3 orientation = new Vector3(0, 0, 0);
            switch (direction)
            {
                case EnumDirection.UP:
                    orientation = new Vector3(0, -90f, 0);
                    break;
                case EnumDirection.DOWN:
                    orientation = new Vector3(0, 90f, 0);
                    break;
                case EnumDirection.LEFT:
                    orientation = new Vector3(0, 180f, 0);
                    break;
            }
            (float X, float Y, float Z) = VOPosition.ConvertToPosition(((int)coordinates.Value.X, (int)coordinates.Value.Y));
            Vector3 position = new Vector3(X, Y, Z);
            return new ArrowDto(id, orientation, position, path.Value);
        }
    }
}
