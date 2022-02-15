using UniMediator;
using Domain.Types;

namespace Usecases.Commands
{
    public interface ICreateArrowCommand
    {
        public EnumDirection _direction { get; }
        public (int, int) _position { get; }
    }
    public class CreateArrowCommand : IMulticastMessage, ICreateArrowCommand
    {
        public EnumDirection _direction { get; private set; }
        public (int, int) _position { get; private set; }

        public CreateArrowCommand(EnumDirection direction, (int, int) position)
        {
            _direction = direction;
            _position = position;
        }
    }
}
