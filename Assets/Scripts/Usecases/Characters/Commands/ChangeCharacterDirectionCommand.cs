using System;
using UniMediator;
using Domain.Characters.Types;

namespace Usecases.Characters.Commands
{
    public interface IChangeCharacterDirectionCommand
    {
        Guid _characterId { get; }
        EnumCharacterDirection _direction { get; }
    }
    public class ChangeCharacterDirectionCommand : ISingleMessage<EnumCharacterDirection>, IChangeCharacterDirectionCommand
    {
        public Guid _characterId { get; }
        public EnumCharacterDirection _direction { get; }

        public ChangeCharacterDirectionCommand(Guid characterId, EnumCharacterDirection direction)
        {
            _characterId = characterId;
            _direction = direction;
        }
    }
}
