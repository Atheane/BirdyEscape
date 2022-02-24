using System;
using UniMediator;

namespace Usecases.Commands
{
    public interface IDeleteArrowCommand
    {
        public Guid _tileId { get; }
    }
    public class DeleteArrowCommand : IMulticastMessage, IDeleteArrowCommand
    {
        public Guid _tileId { get; private set; }

        public DeleteArrowCommand(Guid tileId)
        {
            _tileId = tileId;
        }
    }
}

