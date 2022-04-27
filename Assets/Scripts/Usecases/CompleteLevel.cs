using UnityEngine;
using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Usecases.Commands;
using Domain.Entities;
using Domain.Repositories;

namespace Usecases
{
    public class CompleteLevel : IUsecase<ICompleteLevelCommand, IGameEntity>
    {
        public ILevelsRepository _levelsRepository;
        public ITilesRepository _tilesRepository;
        public ICharactersRepository _charactersRepository;
        public IGameRepository _gameRepository;
        public IDomainEventDispatcher _domainEventDispatcher;

        public CompleteLevel(
            ILevelsRepository levelsRepository,
            ITilesRepository tilesRepository,
            ICharactersRepository charactersRepository,
            IGameRepository gameRepository,
            IDomainEventDispatcher domainEventDispatcher
        )
        {
            _levelsRepository = levelsRepository;
            _tilesRepository = tilesRepository;
            _charactersRepository = charactersRepository;
            _gameRepository = gameRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }
        public IGameEntity Execute(ICompleteLevelCommand command)
        {
            var levelEntity = _levelsRepository.Find(command._id);
            IGameEntity gameEntity = _gameRepository.Load(levelEntity);
            gameEntity.CompleteLevel();
            _gameRepository.Save(gameEntity);
            _domainEventDispatcher.Dispatch(gameEntity);
            return gameEntity;
        }
    }
}
