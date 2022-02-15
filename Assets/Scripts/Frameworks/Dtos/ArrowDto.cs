using System;
using UnityEngine;
using Domain.Types;

namespace Frameworks.Dtos
{
    public interface IArrowDto
    {
        public Guid _id { get; }
        public EnumDirection _direction { get; }
        public Vector2 _coordinates { get; }
    }

    public class ArrowDto : IArrowDto
    {
        public Guid _id { get; private set; }
        public EnumDirection _direction { get; private set; }
        public Vector2 _coordinates { get; private set; }

        private ArrowDto(Guid id, EnumDirection direction, Vector2 coordinates)
        {
            _id = id;
            _direction = direction;
            _coordinates = coordinates;
        }

        public static ArrowDto Create(Guid id, EnumDirection direction, Vector2 coordinates)
        {
            var tileDto = new ArrowDto(id, direction, coordinates);
            return tileDto;
        }

    }
}
