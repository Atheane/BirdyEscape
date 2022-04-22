using UnityEngine;
using System;
using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Usecases.Commands;
using Domain.Entities;
using Domain.Repositories;

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

            if (command._number == 1)
            {
                try
                {
                    _gameRepository.Load();
                } catch(Exception e)
                {
                    var game = GameEntity.Create(levelEntity);
                    _domainEventDispatcher.Dispatch(game);
                    _gameRepository.Save(game);
                }
            }

            return levelEntity;
        }
    }
}
