using System;
using UnityEngine;
using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Domain.Entities;
using Domain.Repositories;

namespace Usecases
{
    public class LoadGame : IUsecase<ILevelEntity, IGameEntity>
    {
        public IGameRepository _gameRepository;
        public IDomainEventDispatcher _domainEventDispatcher;

        public LoadGame(
            IGameRepository gameRepository,
            IDomainEventDispatcher domainEventDispatcher
        )
        {
            _gameRepository = gameRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }
        public IGameEntity Execute(ILevelEntity levelEntity)
        {
            IGameEntity game = _gameRepository.Load(levelEntity);
            _domainEventDispatcher.Dispatch(game);
            return game;
        }
    }
}
