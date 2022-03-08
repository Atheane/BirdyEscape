using System;
using System.Collections.Generic;
using UnityEngine;
using Libs.Adapters;
using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Domain.Repositories;
using Domain.Entities;
using Domain.Types;
using Usecases.Commands;
using Domain.ValueObjects;

namespace Usecases
{
    public class RestartLevel : IUsecase<IRestartLevelCommand, ILevelEntity>
    {
        public ILevelsRepository _levelsRepository;
        public ICharactersRepository _charactersRepository;
        public IDomainEventDispatcher _domainEventDispatcher;
        public ILevelEntity _levelEntity;
        public IMapper<VOPosition, Vector3> _mapper;

        public RestartLevel(
            ILevelsRepository levelsRepository,
            ICharactersRepository charactersRepository,
            IDomainEventDispatcher domainEventDispatcher,
            IMapper<VOPosition, Vector3> mapper
        )
        {
            _levelsRepository = levelsRepository;
            _charactersRepository = charactersRepository;
            _domainEventDispatcher = domainEventDispatcher;
            _mapper = mapper;
        }

        public ILevelEntity Execute(IRestartLevelCommand command)
        {
            _levelEntity = _levelsRepository.Find(command._levelId);
            List<(ICharacterEntity characterEntity, VOPosition position, EnumDirection direction)> charactersProps = new List<(ICharacterEntity characterEntity, VOPosition position, EnumDirection direction)>();
            foreach ((Guid id, Vector3 position, EnumDirection direction) in command._charactersRestartProps)
            {
                var vOPosition = _mapper.ToDomain(position);
                charactersProps.Add((_charactersRepository.Find(id), vOPosition, direction));
            }
            _levelEntity.Restart(charactersProps.ToArray());

            foreach (ICharacterEntity character in _levelEntity._characters)
            {
                _domainEventDispatcher.Dispatch(character);
                _charactersRepository.Update(character);
            }
            _levelsRepository.Update(_levelEntity);
            _domainEventDispatcher.Dispatch(_levelEntity);
            return _levelEntity;
        }

    }
}
