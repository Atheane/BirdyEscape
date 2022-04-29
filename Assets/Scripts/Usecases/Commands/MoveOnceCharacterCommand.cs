using System;
using UniMediator;

namespace Usecases.Commands
{
    public interface IMoveAlwaysCharacterCommand
    {
        Guid _characterId { get; }
    }
    public class MoveOnceCharacterCommand : IMulticastMessage, IMoveAlwaysCharacterCommand
    {
        public Guid _characterId { get; private set; }

        public MoveOnceCharacterCommand(Guid characterId)
        {
            _characterId = characterId;
        }
    }
}