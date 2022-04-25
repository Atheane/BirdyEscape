using System;
using Domain.Types;
using UnityEngine;
using Domain.ValueObjects;


namespace Frameworks.Dtos
{
    public class CharacterDto
    {
        public string _id;
        public EnumCharacterType _type;
        public EnumDirection _direction;
        public EnumCharacterState _state;
        public Vector3 _position;
        public Vector3 _orientation;
        public int _speed;
        public string _image;
        public float _totalDistance;


        private CharacterDto(Guid id, EnumCharacterType type, EnumDirection direction, EnumCharacterState state, Vector3 position, int speed, string image, float totalDistance)
        {
            _id = id.ToString();
            _type = type;
            _direction = direction;
            _state = state;
            _position = position;
            _speed = speed;
            _image = image;
            _totalDistance = totalDistance;
        }

        public static CharacterDto Create(Guid id, EnumCharacterType type, EnumDirection direction, EnumCharacterState state, VOPosition position, int speed, float totalDistance)
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
            var pos = new Vector3(position.Value.X, PuzzleController.MIN.y, position.Value.Z);
            var characterDto = new CharacterDto(id, type, direction, state, pos, speed, image, totalDistance);
            characterDto.Orientate();
            return characterDto;
        }

        public void Orientate()
        {
            switch (_direction)
            {
                case EnumDirection.UP:
                    _orientation = new Vector3(0, -90f, 0);
                    break;
                case EnumDirection.DOWN:
                    _orientation = new Vector3(0, 90f, 0);
                    break;
                case EnumDirection.LEFT:
                    _orientation = new Vector3(0, 180f, 0);
                    break;
                case EnumDirection.RIGHT:
                    _orientation = new Vector3(0, 0, 0);
                    break;
            }
        }

        public void UpdateDirection(EnumDirection direction)
        {
            _direction = direction;
        }
    }
}