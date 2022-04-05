using System;
using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Domain.Entities;
using Domain.Repositories;

namespace Usecases
{
    public class LoadGame : IUsecase<IntPtr, IGameEntity>
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
        public IGameEntity Execute(IntPtr pointer)
        {
            var gameEntity = GameEntity.Create(1, 500);
            _gameRepository.Add(gameEntity);
            _domainEventDispatcher.Dispatch(gameEntity);
            return gameEntity;
        }
    }
}
