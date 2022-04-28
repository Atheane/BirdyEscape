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
    public class CreateLevel : IUsecase<ICreateLevelCommand, ILevelEntity>
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
        public ILevelEntity Execute(ICreateLevelCommand command)
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
            }
            catch(Exception e)
            {
                if (e.InnerException is AppException.FileNotFound)
                {
                    var game = GameEntity.Create(levelEntity, VOEnergy.Create(), new List<DateTime>());
                    _domainEventDispatcher.Dispatch(game);
                    _gameRepository.Save(game);
                } else
                {
                    throw e;
                }
            }

            return levelEntity;
        }
    }
}
