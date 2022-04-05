using System;
using UniMediator;

namespace Usecases.Commands
{
    public interface IUpdateGameCommand
    {
        public int _currentLevelNumber { get; }
        public Guid _currentLevelId { get; }
    }
    public class UpdateGameCommand : IMulticastMessage, IUpdateGameCommand
    {
        public int _currentLevelNumber { get; private set; }
        public Guid _currentLevelId { get; }

        public UpdateGameCommand(int currentLevelNumber, Guid currentLevelId)
        {
            _currentLevelNumber = currentLevelNumber;
            _currentLevelId = currentLevelId;
        }
    }
}
