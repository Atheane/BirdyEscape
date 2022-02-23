using System;
using UniMediator;

namespace Usecases.Commands
{
    public interface IDeleteArrowCommand
    {
        public Guid _arrowId { get; }
    }
    public class DeleteArrowCommand : IMulticastMessage, IDeleteArrowCommand
    {
        public Guid _arrowId { get; private set; }

        public DeleteArrowCommand(Guid arrowId)
        {
            _arrowId = arrowId;
        }
    }
}

