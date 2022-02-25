using System;
using UniMediator;
using Domain.Types;

namespace Usecases.Commands
{
    public interface IUpdateTileArrowDirectionCommand
    {
        Guid _tileId { get; }
        EnumDirection _direction { get; }
    }
    public class UpdateTileArrowDirectionCommand : IMulticastMessage, IUpdateTileArrowDirectionCommand
    {
        public Guid _tileId { get; }
        public EnumDirection _direction { get; }


        public UpdateTileArrowDirectionCommand(Guid tileId, EnumDirection direction)
        {
            _tileId = tileId;
            _direction = direction;
        }
    }
}
