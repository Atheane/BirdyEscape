using System;
using UniMediator;
using Domain.Types;

namespace Usecases.Commands
{
    public interface IUpdateLevelStateCommand
    {
        public Guid _levelId { get; }
        public EnumLevelState _state { get; }
    }
    public class UpdateLevelStateCommand : IMulticastMessage, IUpdateLevelStateCommand
    {
        public Guid _levelId { get; private set; }
        public EnumLevelState _state { get; private set; }


        public UpdateLevelStateCommand(Guid levelId, EnumLevelState state)
        {
            _levelId = levelId;
            _state = state;
        }
    }
}

