using System;
using UnityEngine;
using UniMediator;
using Domain.Types;

namespace Usecases.Commands
{
    public interface IRestartLevelCommand
    {
        Guid _levelId { get; }
        (Guid id, Vector3 position, EnumDirection direction)[] _charactersRestartProps { get; }
    }
    public class RestartLevelCommand : IMulticastMessage, IRestartLevelCommand
    {
        public Guid _levelId { get; private set; }
        public (Guid id, Vector3 position, EnumDirection direction)[] _charactersRestartProps { get; private set; }

        public RestartLevelCommand(Guid levelId, (Guid id, Vector3 position, EnumDirection direction)[] charactersRestartProps)
        {
            _levelId = levelId;
            _charactersRestartProps = charactersRestartProps;
        }
    }
}

