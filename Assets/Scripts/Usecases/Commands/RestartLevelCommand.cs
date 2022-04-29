using System;
using UniMediator;

namespace Usecases.Commands
{
    public interface IRestartLevelCommand
    {
        Guid _levelId { get; }
    }
    public class RestartLevelCommand : IMulticastMessage, IRestartLevelCommand
    {
        public Guid _levelId { get; private set; }

        public RestartLevelCommand(Guid levelId)
        {
            _levelId = levelId;
        }
    }
}

