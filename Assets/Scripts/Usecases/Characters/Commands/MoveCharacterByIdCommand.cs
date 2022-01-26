using System;
using UniMediator;
using Domain.Characters.ValueObjects;

namespace Usecases.Characters.Commands
{
    public interface IMoveAlwaysCharacterByIdCommand
    {
        Guid CharacterId { get; }
    }
    public class MoveAlwaysCharacterByIdCommand : ISingleMessage<VOPosition>, IMoveAlwaysCharacterByIdCommand
    {
        public Guid CharacterId { get; }

        public MoveAlwaysCharacterByIdCommand(Guid characterId)
        {
            CharacterId = characterId;
        }
    }
}