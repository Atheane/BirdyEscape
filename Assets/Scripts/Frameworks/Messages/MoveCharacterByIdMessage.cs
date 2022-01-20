using System;
using UniMediator;
using Usecases.Characters;
using Domain.Characters.Types;

namespace Frameworks.Messages
{
    public class MoveCharacterByIdMessage : ISingleMessage<EnumCharacterState>, IMoveCharacterByIdCommand
    {
        public Guid CharacterId { get; }

        public MoveCharacterByIdMessage(Guid characterId)
        {
            this.CharacterId = characterId;
        }
    }
}