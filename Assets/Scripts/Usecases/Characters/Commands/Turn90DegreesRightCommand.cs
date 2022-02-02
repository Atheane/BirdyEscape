using System;
using UniMediator;

namespace Usecases.Characters.Commands
{
    public interface ITurn90DegreesRightCommand
    {
        Guid _characterId { get; }
    }
    public class Turn90DegreesRightCommand : IMulticastMessage, ITurn90DegreesRightCommand
    {
        public Guid _characterId { get; }

        public Turn90DegreesRightCommand(Guid characterId)
        {
            _characterId = characterId;
        }
    }
}