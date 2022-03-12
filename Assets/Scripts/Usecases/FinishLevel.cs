using UnityEngine;
using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Usecases.Commands;
using Domain.Entities;
using Domain.Repositories;

namespace Usecases
{
    public class FinishLevel : IUsecase<IFinishLevelCommand, ILevelEntity>
    {
        public ILevelsRepository _levelsRepository;
        public ITilesRepository _tilesRepository;
        public ICharactersRepository _charactersRepository;
        public IDomainEventDispatcher _domainEventDispatcher;

        public FinishLevel(
            ILevelsRepository levelsRepository,
            ITilesRepository tilesRepository,
            ICharactersRepository charactersRepository,
            IDomainEventDispatcher domainEventDispatcher
        )
        {
            _levelsRepository = levelsRepository;
            _tilesRepository = tilesRepository;
            _charactersRepository = charactersRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }
        public ILevelEntity Execute(IFinishLevelCommand command)
        {
            var levelEntity = _levelsRepository.Find(command._id);
            levelEntity.Finish();
            foreach (ICharacterEntity character in levelEntity._characters)
            {
                _charactersRepository.Remove(character);
                _domainEventDispatcher.Dispatch(character);
            }
            foreach (ITileEntity tile in levelEntity._tiles)
            {
                _tilesRepository.Remove(tile);
                _domainEventDispatcher.Dispatch(tile);
            }

            _levelsRepository.Update(levelEntity);
            _domainEventDispatcher.Dispatch(levelEntity);
            return levelEntity;
        }
    }
}
