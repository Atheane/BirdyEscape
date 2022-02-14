using System;
using UniMediator;
using Domain.ValueObjects;

namespace Usecases.Commands
{
    public interface IMoveAlwaysCharacterCommand
    {
        Guid _characterId { get; }
    }
    public class MoveAlwaysCharacterCommand : ISingleMessage<VOPosition3D>, IMoveAlwaysCharacterCommand
    {
        public Guid _characterId { get; }

        public MoveAlwaysCharacterCommand(Guid characterId)
        {
            _characterId = characterId;
        }
    }
}