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
        public ILevelsRepository _levelsRepository;
        public IDomainEventDispatcher _domainEventDispatcher;

        public SaveGame(
            IGameRepository gameRepository,
            ILevelsRepository levelsRepository,
            IDomainEventDispatcher domainEventDispatcher
        )
        {
            _gameRepository = gameRepository;
            _levelsRepository = levelsRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }
        public IGameEntity Execute(IUpdateGameCommand command)
        {
            ILevelEntity level = _levelsRepository.Find(command._currentLevelId);
            IGameEntity game = _gameRepository.Load(level);

            game.UpdateCurrentLevel(level);
            game.ComputeEnergy();
            _gameRepository.Save(game);
            _domainEventDispatcher.Dispatch(game);
            return game;
        }
    }
}
