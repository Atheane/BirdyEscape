using System;
using UniMediator;
using Domain.Characters.Types;

namespace Usecases.Characters.Commands
{
    public interface ITurnCharacter90DegreesCommand
    {
        Guid _characterId { get; }
    }
    public class TurnCharacter90DegreesCommand : ISingleMessage<EnumCharacterDirection>, ITurnCharacter90DegreesCommand
    {
        public Guid _characterId { get; }

        public TurnCharacter90DegreesCommand(Guid characterId)
        {
            _characterId = characterId;
        }
    }
}
