using System;
using UniMediator;
using Domain.Types;

namespace Usecases.Commands
{
    public interface IUpdateArrowDirectionCommand
    {
        Guid _tileId { get; }
        EnumDirection _direction { get; }
    }
    public class UpdateArrowDirectionCommand : IMulticastMessage, IUpdateArrowDirectionCommand
    {
        public Guid _tileId { get; }
        public EnumDirection _direction { get; }


        public UpdateArrowDirectionCommand(Guid tileId, EnumDirection direction)
        {
            _tileId = tileId;
            _direction = direction;
        }
    }
}
