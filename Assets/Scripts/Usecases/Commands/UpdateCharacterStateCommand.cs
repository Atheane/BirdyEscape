using System;
using UniMediator;
using Domain.Types;

namespace Usecases.Commands
{
    public interface IUpdateCharacterStateCommand
    {
        Guid _characterId { get; }
        EnumCharacterState _state { get; }
    }
    public class UpdateCharacterStateCommand : IMulticastMessage, IUpdateCharacterStateCommand
    {
        public Guid _characterId { get; }
        public EnumCharacterState _state { get; }


        public UpdateCharacterStateCommand(Guid characterId, EnumCharacterState state)
        {
            _characterId = characterId;
            _state = state;
        }
    }
}
