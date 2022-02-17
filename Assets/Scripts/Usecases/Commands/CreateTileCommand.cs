using UniMediator;

namespace Usecases.Commands
{
    public interface ICreateTileCommand
    {
        public (float, float, float) _position { get; }
        public string _path { get; }
    }
    public class CreateTileCommand : IMulticastMessage, ICreateTileCommand
    {
        public (float, float, float) _position { get; private set; }
        public string _path { get; private set; }

        public CreateTileCommand((float, float, float) position, string path)
        {
            _position = position;
            _path = path;
        }
    }
}

