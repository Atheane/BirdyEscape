using System;
using UniMediator;
using Usecases.Characters;
using Domain.Characters.ValueObjects;

namespace Frameworks.Messages
{
    public class MoveAlwaysCharacterByIdMessage : ISingleMessage<VOPosition>, IMoveAlwaysCharacterByIdCommand
    {
        public Guid CharacterId { get; }

        public MoveAlwaysCharacterByIdMessage(Guid characterId)
        {
            this.CharacterId = characterId;
        }
    }
}