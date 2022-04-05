using System;
using UnityEngine;
using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Domain.Entities;
using Domain.Repositories;

namespace Usecases
{
    public class LoadOrCreateGame : IUsecase<IntPtr, IGameEntity>
    {
        public IGameRepository _gameRepository;
        public IDomainEventDispatcher _domainEventDispatcher;

        public LoadOrCreateGame(
            IGameRepository gameRepository,
            IDomainEventDispatcher domainEventDispatcher
        )
        {
            _gameRepository = gameRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }
        public IGameEntity Execute(IntPtr pointer)
        {
            try
            {
                return _gameRepository.Load();
            } catch(Exception exception)
            {
                Debug.Log(exception);
                var gameEntity = GameEntity.Create(1, 500);
                _gameRepository.Save(gameEntity);
                _domainEventDispatcher.Dispatch(gameEntity);
                return gameEntity;
            }
        }
    }
}
