using System;
using UniMediator;
using Domain.Types;

namespace Usecases.Commands
{
    public interface IUpdateDirectionCommand
    {
        Guid _characterId { get; }
        EnumDirection _direction { get; }
    }
    public class UpdateDirectionCommand : ISingleMessage<string>, IUpdateDirectionCommand
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