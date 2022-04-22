using System;
using UnityEngine;
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
            IGameEntity game = _gameRepository.Load();
            _domainEventDispatcher.Dispatch(game);
            return game;
        }
    }
}
