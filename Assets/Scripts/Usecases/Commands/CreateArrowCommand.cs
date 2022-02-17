using UniMediator;
using Domain.Types;

namespace Usecases.Commands
{
    public interface ICreateArrowCommand
    {
        public EnumDirection _direction { get; }
        public (int, int) _coordinates { get; }
        public string _path { get;  }
    }
    public class CreateArrowCommand : IMulticastMessage, ICreateArrowCommand
    {
        public EnumDirection _direction { get; private set; }
        public (int, int) _coordinates { get; private set; }
        public string _path { get; private set; }

        public CreateArrowCommand(EnumDirection direction, (int, int) coords, string path)
        {
            _direction = direction;
            _coordinates = coords;
            _path = path;
        }
    }
}
