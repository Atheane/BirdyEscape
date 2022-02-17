using System;
using UniMediator;
using Domain.ValueObjects;

namespace Usecases.Commands
{
    public interface IMoveAlwaysCharacterCommand
    {
        Guid _characterId { get; }
    }
    public class MoveAlwaysCharacterCommand : ISingleMessage<VOPosition>, IMoveAlwaysCharacterCommand
    {
        public Guid _characterId { get; private set; }

        public MoveAlwaysCharacterCommand(Guid characterId)
        {
            _characterId = characterId;
        }
    }
}