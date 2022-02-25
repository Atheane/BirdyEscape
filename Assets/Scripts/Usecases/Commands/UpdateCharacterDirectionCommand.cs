using System;
using UniMediator;
using Domain.Types;

namespace Usecases.Commands
{
    public interface IUpdateCharacterDirectionCommand
    {
        Guid _characterId { get; }
        EnumDirection _direction { get; }
    }
    public class UpdateCharacterDirectionCommand : IMulticastMessage, IUpdateCharacterDirectionCommand
    {
        public Guid _characterId { get; }
        public EnumDirection _direction { get; }


        public UpdateCharacterDirectionCommand(Guid characterId, EnumDirection direction)
        {
            _characterId = characterId;
            _direction = direction;
        }
    }
}