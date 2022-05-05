using System;
using UniMediator;
using Domain.Types;

namespace Usecases.Commands
{
    public interface IAddTileArrowCommand
    {
        public Guid _tileId { get; }
        public EnumDirection _direction { get; }
        public string _path { get;  }
        public bool _effectOnce { get; }
    }
    public class AddTileArrowCommand : IMulticastMessage, IAddTileArrowCommand
    {
        public Guid _tileId { get; private set; }
        public EnumDirection _direction { get; private set; }
        public string _path { get; private set; }
        public bool _effectOnce { get; private set; }

        public AddTileArrowCommand(Guid tileId, EnumDirection direction, string path, bool effectOnce)
        {
            _tileId = tileId;
            _direction = direction;
            _path = path;
            _effectOnce = effectOnce;
        }
    }
}
