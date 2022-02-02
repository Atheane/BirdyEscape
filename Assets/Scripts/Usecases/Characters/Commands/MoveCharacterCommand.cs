using System;
using UniMediator;
using Domain.Characters.ValueObjects;

namespace Usecases.Characters.Commands
{
    public interface IMoveAlwaysCharacterCommand
    {
        Guid _characterId { get; }
    }
    public class MoveAlwaysCharacterCommand : ISingleMessage<VOPosition>, IMoveAlwaysCharacterCommand
    {
        public Guid _characterId { get; }

        public MoveAlwaysCharacterCommand(Guid characterId)
        {
            _characterId = characterId;
        }
    }
}