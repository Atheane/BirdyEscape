using System;
using UniMediator;
using Domain.Characters.ValueObjects;

namespace Usecases.Characters.Commands
{
    public interface IMoveAlwaysCharacterCommand
    {
        Guid CharacterId { get; }
    }
    public class MoveAlwaysCharacterCommand : ISingleMessage<VOPosition>, IMoveAlwaysCharacterCommand
    {
        public Guid CharacterId { get; }

        public MoveAlwaysCharacterCommand(Guid characterId)
        {
            CharacterId = characterId;
        }
    }
}