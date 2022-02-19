using System;
using UniMediator;

namespace Usecases.Commands
{
    public interface ITurnRightCommand
    {
        Guid _characterId { get; }
    }
    public class TurnRightCommand : ISingleMessage<string>, ITurnRightCommand
    {
        public Guid _characterId { get; private set; }

        public TurnRightCommand(Guid characterId)
        {
            _characterId = characterId;
        }
    }
}