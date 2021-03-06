using UnityEngine;
using System;
using System.Collections.Generic;
using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using Domain.Exceptions;
using Adapters.Exceptions;
using Usecases.Commands;

namespace Usecases
{
    public class CreateLevel : IUsecase<ICreateLevelCommand, IGameEntity>
    {
        public ILevelsRepository _levelsRepository;
        public IGameRepository _gameRepository;
        public IDomainEventDispatcher _domainEventDispatcher;

        public CreateLevel(
            ILevelsRepository levelsRepository,
            IGameRepository gameRepository,
            IDomainEventDispatcher domainEventDispatcher
        )
        {
            _levelsRepository = levelsRepository;
            _gameRepository = gameRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }
        public IGameEntity Execute(ICreateLevelCommand command)
        {
            var levelEntity = LevelEntity.Create(
                command._number,
                command._characters.ToArray(),
                command._tiles.ToArray(),
                command._state);
            _levelsRepository.Add(levelEntity);
            _domainEventDispatcher.Dispatch(levelEntity);

            try
            {
                var game = _gameRepository.Load(levelEntity);
                game.ComputeEnergy();
                _domainEventDispatcher.Dispatch(game);
                _gameRepository.Save(game);
                return game;
            }
            catch(Exception e)
            {
                Debug.Log(e);
                var game = GameEntity.Create(levelEntity, VOEnergy.Create(), new List<DateTime>());
                _domainEventDispatcher.Dispatch(game);
                _gameRepository.Save(game);
                return game;
            }
        }
    }
}
