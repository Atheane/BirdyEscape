using System;
using UniMediator;

namespace Usecases.Commands
{
    public interface ILevelPlayMoveCommand
    {
        Guid _levelId { get; }
    }
    public class LevelPlayMoveCommand : IMulticastMessage, ILevelPlayMoveCommand
    {
        public Guid _levelId { get; }

        public LevelPlayMoveCommand(Guid levelId)
        {
            _levelId = levelId;
        }
    }
}
