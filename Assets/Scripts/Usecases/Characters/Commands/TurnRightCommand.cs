using System;
using UniMediator;

namespace Usecases.Characters.Commands
{
    public interface ITurnRightCommand
    {
        Guid _characterId { get; }
    }
    public class TurnRightCommand : IMulticastMessage, ITurnRightCommand
    {
        public Guid _characterId { get; }

        public TurnRightCommand(Guid characterId)
        {
            _characterId = characterId;
        }
    }
}