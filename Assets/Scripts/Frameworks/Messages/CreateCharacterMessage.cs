using UniMediator;
using Frameworks.Dtos;
using Usecases.Characters;
using Domain.Characters.Types;

namespace Frameworks.Messages
{
    public class CreateCharacterMessage : ISingleMessage<ICharacterDto>, ICreateCharacterCommand
    {
        public EnumCharacterType Type { get; }
        public EnumCharacterDirection Direction { get; }
        public (float, float) Position { get; }
        public float Speed { get; }

        public CreateCharacterMessage(EnumCharacterType type, EnumCharacterDirection direction, (float, float) position, float speed)
        {
            this.Type = type;
            this.Direction = direction;
            this.Position = position;
            this.Speed = speed;
    }
    }
}
