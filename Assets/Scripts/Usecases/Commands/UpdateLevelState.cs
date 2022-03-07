using System;
using UniMediator;
using Domain.Types;

namespace Usecases.Commands
{
    public interface IUpdateLevelStateCommand
    {
        Guid _levelId { get; }
        EnumLevelState _state { get; }
    }
    public class UpdateLevelStateCommand : IMulticastMessage, IUpdateLevelStateCommand
    {
        public Guid _levelId { get; }
        public EnumLevelState _state { get; }


        public UpdateLevelStateCommand(Guid levelId, EnumLevelState state)
        {
            _levelId = levelId;
            _state = state;
        }
    }
}

