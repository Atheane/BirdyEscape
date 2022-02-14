using System;
using Domain.Types;
using UnityEngine;

namespace Frameworks.Dtos
{
    public interface ICharacterDto
    {
        public Guid _id { get; }
        public EnumCharacterType _type { get; }
        public EnumDirection _direction { get; }
        public Vector3 _position { get; }
        public Vector3 _orientation { get; }
        public int _speed { get; }
        public string _image { get; }
    }

    public class CharacterDto: ICharacterDto
    {
        public Guid _id { get; private set; }
        public EnumCharacterType _type { get; private set; }
        public EnumDirection _direction { get; private set; }
        public Vector3 _position { get; private set; }
        public Vector3 _orientation { get; private set; }
        public int _speed { get; private set; }
        public string _image { get; private set; }

        private CharacterDto(Guid id, EnumCharacterType type, EnumDirection direction, Vector3 position, int speed, string image)
        {
            _id = id;
            _type = type;
            _direction = direction;
            _position = position;
            _speed = speed;
            _image = image;
        }

        public static CharacterDto Create(Guid id, EnumCharacterType type, EnumDirection direction, Vector3 position, int speed)
        {
            string image;
            switch (type)
            {
                case EnumCharacterType.BLACK_BIRD:
                    image = "Cute_Bird_16";
                    break;
                default:
                    image = "";
                    break;
            }
            var characterDto = new CharacterDto(id, type, direction, position, speed, image);
            characterDto.Orientate();
            return characterDto;
        }

        public void Orientate()
        {
            switch (_direction)
            {
                case EnumDirection.LEFT:
                    _orientation = new Vector3(0, -90f, 0);
                    break;
                case EnumDirection.RIGHT:
                    _orientation = new Vector3(0, 90f, 0);
                    break;
                case EnumDirection.DOWN:
                    _orientation = new Vector3(0, 180f, 0);
                    break;
                case EnumDirection.UP:
                    _orientation = new Vector3(0, 0, 0);
                    break;
            }
        }
    }
}