using UniMediator;
using Domain.Types;

namespace Usecases.Commands
{
    public interface ICreateCharacterCommand
    {
        public EnumCharacterType Type { get; }
        public EnumDirection Direction { get; }
        public (float, float, float) Position { get; }
        public int Speed { get; }
    }
    public class CreateCharacterCommand : IMulticastMessage, ICreateCharacterCommand
    {
        public EnumCharacterType Type { get; }
        public EnumDirection Direction { get; }
        public (float, float, float) Position { get; }
        public int Speed { get; }

        public CreateCharacterCommand(EnumCharacterType type, EnumDirection direction, (float, float, float) position, int speed)
        {
            Type = type;
            Direction = direction;
            Position = position;
            Speed = speed;
        }
    }
}
