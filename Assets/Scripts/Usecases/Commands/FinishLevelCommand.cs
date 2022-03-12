using System;
using UniMediator;

namespace Usecases.Commands
{
    public interface IFinishLevelCommand
    {
        public Guid _id { get; }
    }
    public class FinishLevelCommand : IMulticastMessage, IFinishLevelCommand
    {
        public Guid _id { get; private set; }

        public FinishLevelCommand(Guid id)
        {
            _id = id;
        }
    }
}

