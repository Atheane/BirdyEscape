using System;
using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Domain.Entities;
using Domain.Repositories;
using Usecases.Commands;
using UnityEngine;

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
            IGameEntity game = _gameRepository.Load();
            game.UpdateLevel(command._currentLevelNumber);
            ILevelEntity level = _levelsRepository.Find(command._currentLevelId);
            game.ComputeEnergy(level);
            _gameRepository.Save(game);
            Debug.Log("_________________2 Save Game");
            Debug.Log(game._energy.Value);
            _domainEventDispatcher.Dispatch(game);
            return game;
        }
    }
}
