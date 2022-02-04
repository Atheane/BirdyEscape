using UniMediator;
using Domain.Characters.Types;

namespace Usecases.Characters.Commands
{
    public interface ICreateCharacterCommand
    {
        public EnumCharacterType Type { get; }
        public EnumCharacterDirection Direction { get; }
        public (float, float, float) Position { get; }
        public int Speed { get; }
    }
    public class CreateCharacterCommand : IMulticastMessage, ICreateCharacterCommand
    {
        public EnumCharacterType Type { get; }
        public EnumCharacterDirection Direction { get; }
        public (float, float, float) Position { get; }
        public int Speed { get; }

        public CreateCharacterCommand(EnumCharacterType type, EnumCharacterDirection direction, (float, float, float) position, int speed)
        {
            Type = type;
            Direction = direction;
            Position = position;
            Speed = speed;
        }
    }
}
