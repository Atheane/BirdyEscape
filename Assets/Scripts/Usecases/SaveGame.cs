using System;
using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Domain.Entities;
using Domain.Repositories;
using Usecases.Commands;

namespace Usecases
{
    public class SaveGame : IUsecase<IUpdateGameCommand, IGameEntity>
    {
        public IGameRepository _gameRepository;
        public IDomainEventDispatcher _domainEventDispatcher;

        public SaveGame(
            IGameRepository gameRepository,
            IDomainEventDispatcher domainEventDispatcher
        )
        {
            _gameRepository = gameRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }
        public IGameEntity Execute(IUpdateGameCommand command)
        {
            IGameEntity game = _gameRepository.Load();
            game.UpdateEnergy(command._energy);
            game.UpdateLevel(command._currentLevel);
            _gameRepository.Save(game);
            _domainEventDispatcher.Dispatch(game);
            return game;
        }
    }
}
