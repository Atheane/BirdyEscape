using UniMediator;

namespace Usecases.Commands
{
    public interface ICreateTileCommand
    {
        public (float, float, float) _position { get; }
        public string _image { get; }
    }
    public class CreateTileCommand : IMulticastMessage, ICreateTileCommand
    {
        public (float, float, float) _position { get; }
        public string _image { get; }

        public CreateTileCommand((float, float, float) position, string image)
        {
            _position = position;
            _image = image;
        }
    }
}

