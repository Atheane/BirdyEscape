using System;
using UniMediator;

namespace Usecases.Commands
{
    public interface IRemoveTileArrowCommand
    {
        public Guid _tileId { get; }
    }
    public class RemoveTileArrowCommand : IMulticastMessage, IRemoveTileArrowCommand
    {
        public Guid _tileId { get; private set; }

        public RemoveTileArrowCommand(Guid tileId)
        {
            _tileId = tileId;
        }
    }
}

