using UniMediator;
using Frameworks.Dtos;
using Usecases.Characters;
using Domain.Characters.Types;

namespace Frameworks.Messages
{
    public class CreateCharacterMessage : ISingleMessage<ICharacterDto>, ICreateCharacterCommand
    {
        public EnumCharacter Type { get; }
        public EnumDirection Direction { get; }
        public (float, float) Position { get; }
        public float Speed { get; }

        public CreateCharacterMessage(EnumCharacter type, EnumDirection direction, (float, float) position, float speed)
        {
            this.Type = type;
            this.Direction = direction;
            this.Position = position;
            this.Speed = speed;
    }
    }
}
