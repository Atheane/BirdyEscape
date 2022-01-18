using System;
using Domain.Characters.Types;

namespace Frameworks.Dtos
{
    public interface ICharacterDto
    {
        public Guid Id { get; }
        public EnumCharacter Type { get; }
        public EnumDirection Direction { get; }
        public (double, double) Position { get; }
        public float Speed { get; }
        public string Image { get; }
    }

    public class CharacterDto: ICharacterDto
    {
        public Guid Id { get; private set; }
        public EnumCharacter Type { get; private set; }
        public EnumDirection Direction { get; private set; }
        public (double, double) Position { get; private set; }
        public float Speed { get; private set; }
        public string Image { get; private set; }

        private CharacterDto(Guid id, EnumCharacter type, EnumDirection direction, (double, double) position, float speed, string image)
        {
            this.Id = id;
            this.Type = type;
            this.Direction = direction;
            this.Position = position;
            this.Speed = speed;
            this.Image = image;
        }

        public static CharacterDto Create(Guid id, EnumCharacter type, EnumDirection direction, (double, double) position, float speed)
        {
            string image;
            switch (type)
            {
                case EnumCharacter.COW:
                    image = "src/cow";
                    break;
                default:
                    image = "";
                    break;
            }
            var characterDto = new CharacterDto(id, type, direction, position, speed, image);

            return characterDto;
        }
    }
}