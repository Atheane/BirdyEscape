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
                IGameEntity game = _gameRepository.Load();
                return game;
            } catch(Exception exception)
            {
                Debug.Log(exception);
                IGameEntity game = GameEntity.Create(1, 500);
                _gameRepository.Save(game);
                _domainEventDispatcher.Dispatch(game);
                return game;
            }
        }
    }
}
