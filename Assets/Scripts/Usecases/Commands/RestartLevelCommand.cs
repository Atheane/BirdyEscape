using System;
using UnityEngine;
using UniMediator;
using Domain.Types;

namespace Usecases.Commands
{
    public interface IRestartLevelCommand
    {
        Guid _levelId { get; }
        (Guid id, Vector3 position, EnumDirection direction, float totalDistance)[] _charactersRestartProps { get; }
        Guid[] _tilesIds { get; }
    }
    public class RestartLevelCommand : IMulticastMessage, IRestartLevelCommand
    {
        public Guid _levelId { get; private set; }
        public (Guid id, Vector3 position, EnumDirection direction, float totalDistance)[] _charactersRestartProps { get; private set; }
        public Guid[] _tilesIds { get; }

        public RestartLevelCommand(Guid levelId, (Guid id, Vector3 position, EnumDirection direction, float totalDistance)[] charactersRestartProps, Guid[] tilesIds)
        {
            _levelId = levelId;
            _charactersRestartProps = charactersRestartProps;
            _tilesIds = tilesIds;
        }
    }
}

