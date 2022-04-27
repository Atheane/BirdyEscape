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
    public class RestartLevel : IUsecase<IRestartLevelCommand, IGameEntity>
    {
        public ILevelsRepository _levelsRepository;
        public ICharactersRepository _charactersRepository;
        public ITilesRepository _tilesRepository;
        public IGameRepository _gameRepository;
        public IDomainEventDispatcher _domainEventDispatcher;
        public IMapper<VOPosition, Vector3> _mapper;

        public RestartLevel(
            ILevelsRepository levelsRepository,
            ICharactersRepository charactersRepository,
            ITilesRepository tilesRepository,
            IGameRepository gameRepository,
            IDomainEventDispatcher domainEventDispatcher,
            IMapper<VOPosition, Vector3> mapper
        )
        {
            _levelsRepository = levelsRepository;
            _charactersRepository = charactersRepository;
            _tilesRepository = tilesRepository;
            _gameRepository = gameRepository;
            _domainEventDispatcher = domainEventDispatcher;
            _mapper = mapper;
        }

        public IGameEntity Execute(IRestartLevelCommand command)
        {
            ILevelEntity levelEntity = _levelsRepository.Find(command._levelId);

            IGameEntity game = _gameRepository.Load(levelEntity);
            game.ComputeEnergy();
            _gameRepository.Save(game);
            _domainEventDispatcher.Dispatch(game);

            var updatedTiles = levelEntity.Restart();

            foreach (ICharacterEntity character in levelEntity._characters)
            {
                _domainEventDispatcher.Dispatch(character);
                _charactersRepository.Update(character);
            }
            foreach (ITileEntity tile in updatedTiles)
            {
                _domainEventDispatcher.Dispatch(tile);
                _tilesRepository.Update(tile);
            }

            _levelsRepository.Update(levelEntity);
            _domainEventDispatcher.Dispatch(levelEntity);


            return game;
        }

    }
}
