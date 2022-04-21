using System;
using UniMediator;

namespace Usecases.Commands
{
    public interface ICompleteLevelCommand
    {
        public Guid _id { get; }
    }
    public class CompleteLevelCommand : IMulticastMessage, ICompleteLevelCommand
    {
        public Guid _id { get; private set; }

        public CompleteLevelCommand(Guid id)
        {
            _id = id;
        }
    }
}

