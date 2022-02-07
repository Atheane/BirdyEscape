using System;
using Domain.Characters.Types;
using UnityEngine;

namespace Frameworks.Dtos
{
    public interface ICharacterDto
    {
        public Guid Id { get; }
        public EnumCharacterType Type { get; }
        public EnumCharacterDirection Direction { get; }
        public Vector3 Position { get; }
        public Vector3 Orientation { get; }
        public int Speed { get; }
        public string Image { get; }
    }

    public class CharacterDto: ICharacterDto
    {
        public Guid Id { get; private set; }
        public EnumCharacterType Type { get; private set; }
        public EnumCharacterDirection Direction { get; private set; }
        public Vector3 Position { get; private set; }
        public Vector3 Orientation { get; private set; }
        public int Speed { get; private set; }
        public string Image { get; private set; }

        private CharacterDto(Guid id, EnumCharacterType type, EnumCharacterDirection direction, Vector3 position, int speed, string image)
        {
            this.Id = id;
            this.Type = type;
            this.Direction = direction;
            this.Position = position;
            this.Speed = speed;
            this.Image = image;
        }

        public static CharacterDto Create(Guid id, EnumCharacterType type, EnumCharacterDirection direction, Vector3 position, int speed)
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
            switch (Direction)
            {
                case EnumCharacterDirection.LEFT:
                    Orientation = new Vector3(0, -90f, 0);
                    break;
                case EnumCharacterDirection.RIGHT:
                    Orientation = new Vector3(0, 90f, 0);
                    break;
                case EnumCharacterDirection.DOWN:
                    Orientation = new Vector3(0, 180f, 0);
                    break;
                case EnumCharacterDirection.UP:
                    Orientation = new Vector3(0, 0, 0);
                    break;
            }
        }
    }
}