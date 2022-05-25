using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Usecases.Commands;
using Domain.Repositories;
using Domain.Entities;


namespace Usecases
{
    public class LevelPlayMove : IUsecase<ILevelPlayMoveCommand, ILevelEntity>
    {
        public ILevelsRepository _levelsRepository;
        public ICharactersRepository _charactersRepository;
        public IDomainEventDispatcher _domainEventDispatcher;

        public LevelPlayMove(
            ILevelsRepository levelsRepository,
            ICharactersRepository charactersRepository,
            IDomainEventDispatcher domainEventDispatcher
        )
        {
            _levelsRepository = levelsRepository;
            _charactersRepository = charactersRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public ILevelEntity Execute(ILevelPlayMoveCommand command)
        {
            ILevelEntity levelEntity = _levelsRepository.Find(command._levelId);
            levelEntity.PlayMove();
            foreach (ICharacterEntity character in levelEntity._characters)
            {
                _charactersRepository.Update(character);
                _domainEventDispatcher.Dispatch(character);
            }
            _levelsRepository.Update(levelEntity);
            _domainEventDispatcher.Dispatch(levelEntity);
            return levelEntity;
        }
    }
}
