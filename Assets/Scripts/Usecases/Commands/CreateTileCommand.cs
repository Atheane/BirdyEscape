using UniMediator;
using UnityEngine;

namespace Usecases.Commands
{
    public interface ICreateTileCommand
    {
        public Vector3 _position { get; }
        public string _path { get; }
    }
    public class CreateTileCommand : ISingleMessage<string>, ICreateTileCommand
    {
        public Vector3 _position { get; private set; }
        public string _path { get; private set; }

        public CreateTileCommand(Vector3 position, string path)
        {
            _position = position;
            _path = path;
        }
    }
}

