using System;
using UniMediator;
using Domain.Types;

namespace Usecases.Commands
{
    public interface IUpdateArrowDirectionCommand
    {
        Guid _arrowId { get; }
        EnumDirection _direction { get; }
    }
    public class UpdateArrowDirectionCommand : IMulticastMessage, IUpdateArrowDirectionCommand
    {
        public Guid _arrowId { get; }
        public EnumDirection _direction { get; }


        public UpdateArrowDirectionCommand(Guid arrowId, EnumDirection direction)
        {
            _arrowId = arrowId;
            _direction = direction;
        }
    }
}
