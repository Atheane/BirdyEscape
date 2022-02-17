using UniMediator;
using Domain.Types;

namespace Usecases.Commands
{
    public interface ICreateCharacterCommand
    {
        public EnumCharacterType _type { get; }
        public EnumDirection _direction { get; }
        public (float, float, float) _position { get; }
        public int _speed { get; }
    }
    public class CreateCharacterCommand : IMulticastMessage, ICreateCharacterCommand
    {
        public EnumCharacterType _type { get; private set; }
        public EnumDirection _direction { get; private set; }
        public (float, float, float) _position { get; private set; }
        public int _speed { get; private set; }

        public CreateCharacterCommand(EnumCharacterType type, EnumDirection direction, (float, float, float) position, int speed)
        {
            _type = type;
            _direction = direction;
            _position = position;
            _speed = speed;
        }
    }
}
