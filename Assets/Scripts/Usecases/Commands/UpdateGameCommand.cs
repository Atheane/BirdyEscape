using System;
using UniMediator;

namespace Usecases.Commands
{
    public interface IUpdateGameCommand
    {
        public Guid _currentLevelId { get; }
    }
    public class UpdateGameCommand : IMulticastMessage, IUpdateGameCommand
    {
        public Guid _currentLevelId { get; }

        public UpdateGameCommand(int currentLevelNumber, Guid currentLevelId)
        {
            _currentLevelId = currentLevelId;
        }
    }
}
