using System;
using UniMediator;
using Domain.Commons.Types;

namespace Usecases.Characters.Commands
{
    public interface IUpdateDirectionCommand
    {
        Guid _characterId { get; }
        EnumDirection _direction { get; }
    }
    public class UpdateDirectionCommand : IMulticastMessage, IUpdateDirectionCommand
    {
        public Guid _characterId { get; }
        public EnumDirection _direction { get; }


        public UpdateDirectionCommand(Guid characterId, EnumDirection direction)
        {
            _characterId = characterId;
            _direction = direction;
        }
    }
}