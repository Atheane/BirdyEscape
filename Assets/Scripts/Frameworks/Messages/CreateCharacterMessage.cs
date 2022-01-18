using UniMediator;
using Frameworks.Dtos;
using Domain.Characters.Types;

namespace Frameworks.Messages
{
    public class CreateCharacterMessage : ISingleMessage<ICharacterDto>
    {
        public EnumCharacter Type { get; }
        public EnumDirection Direction { get; }
        public (double, double) Position { get; }
        public float Speed { get; }

        public CreateCharacterMessage(EnumCharacter type, EnumDirection direction, (double, double) position, float speed)
        {
            this.Type = type;
            this.Direction = direction;
            this.Position = position;
            this.Speed = speed;
        }
    }
}
