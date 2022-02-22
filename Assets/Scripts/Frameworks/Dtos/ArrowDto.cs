using System;
using UnityEngine;
using Domain.Types;
using Domain.ValueObjects;
using Domain.Constants;

namespace Frameworks.Dtos
{
    public interface IArrowDto
    {
        public Guid _id { get; }
        public EnumDirection _direction { get; }
        public Vector3 _orientation { get; }
        public Vector3 _position { get; }
        public string _path { get; }
    }

    public class ArrowDto : IArrowDto
    {
        public Guid _id { get; private set; }
        public EnumDirection _direction { get; }
        public Vector3 _orientation { get; private set; }
        public Vector3 _position { get; private set; }
        public string _path { get; }

        private ArrowDto(Guid id, EnumDirection direction, Vector3 orientation, Vector3 position, string path)
        {
            _id = id;
            _direction = direction;
            _orientation = orientation;
            _position = position;
            _path = path;
        }

        public static ArrowDto Create(Guid id, EnumDirection direction, VOCoordinates coordinates, VOPath path)
        {
            Vector3 orientation = new Vector3(0, 90, 0);
            switch (direction)
            {
                case EnumDirection.UP:
                    orientation = new Vector3(0, 0f, 0);
                    break;
                case EnumDirection.DOWN:
                    orientation = new Vector3(0, 180f, 0);
                    break;
                case EnumDirection.LEFT:
                    orientation = new Vector3(0, -90f, 0);
                    break;
            }
            var posX = coordinates.Value.X + 0.5f;
            var posY = Position.INIT_Y;
            var posZ = coordinates.Value.Y;
            Vector3 position = new Vector3(posX, posY, posZ);

            return new ArrowDto(id, direction, orientation, position, path.Value);
        }
    }
}
