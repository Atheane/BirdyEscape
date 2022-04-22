using System;
using System.Collections.Generic;
using UnityEngine;
using Libs.Adapters;
using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Domain.Repositories;
using Domain.Entities;
using Usecases.Commands;
using Domain.ValueObjects;

namespace Usecases
{
    public class RestartLevel : IUsecase<IRestartLevelCommand, ILevelEntity>
    {
        public ILevelsRepository _levelsRepository;
        public ICharactersRepository _charactersRepository;
        public ITilesRepository _tilesRepository;
        public IDomainEventDispatcher _domainEventDispatcher;
        public ILevelEntity _levelEntity;
        public IMapper<VOPosition, Vector3> _mapper;

        public RestartLevel(
            ILevelsRepository levelsRepository,
            ICharactersRepository charactersRepository,
            ITilesRepository tilesRepository,
            IDomainEventDispatcher domainEventDispatcher,
            IMapper<VOPosition, Vector3> mapper
        )
        {
            _levelsRepository = levelsRepository;
            _charactersRepository = charactersRepository;
            _tilesRepository = tilesRepository;
            _domainEventDispatcher = domainEventDispatcher;
            _mapper = mapper;
        }

        public ILevelEntity Execute(IRestartLevelCommand command)
        {
            _levelEntity = _levelsRepository.Find(command._levelId);

            var updatedTiles = _levelEntity.Restart();

            foreach (ICharacterEntity character in _levelEntity._characters)
            {
                _domainEventDispatcher.Dispatch(character);
                _charactersRepository.Update(character);
            }
            foreach (ITileEntity tile in updatedTiles)
            {
                _domainEventDispatcher.Dispatch(tile);
                _tilesRepository.Update(tile);
            }

            _levelsRepository.Update(_levelEntity);
            _domainEventDispatcher.Dispatch(_levelEntity);
            return _levelEntity;
        }

    }
}
