using UniMediator;
using Frameworks.Dtos;
using Domain.Characters.Types;

namespace Usecases.Characters.Commands
{
    public interface ICreateCharacterCommand
    {
        public EnumCharacterType Type { get; }
        public EnumCharacterDirection Direction { get; }
        public (float, float) Position { get; }
        public int Speed { get; }
    }
    public class CreateCharacterCommand : ISingleMessage<ICharacterDto>, ICreateCharacterCommand
    {
        public EnumCharacterType Type { get; }
        public EnumCharacterDirection Direction { get; }
        public (float, float) Position { get; }
        public int Speed { get; }

        public CreateCharacterCommand(EnumCharacterType type, EnumCharacterDirection direction, (float, float) position, int speed)
        {
            Type = type;
            Direction = direction;
            Position = position;
            Speed = speed;
        }
    }
}
