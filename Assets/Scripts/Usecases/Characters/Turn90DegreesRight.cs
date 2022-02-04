using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Usecases.Characters.Commands;
using Domain.Characters.Repositories;
using Domain.Characters.Types;
using Domain.Characters.Entities;


namespace Usecases.Characters
{
    public class Turn90DegreesRight : IUsecase<ITurn90DegreesRightCommand, EnumCharacterDirection>
    {
        public ICharactersRepository _charactersRepository;
        public IDomainEventDispatcher _domainEventDispatcher;

        public Turn90DegreesRight(
            ICharactersRepository charactersRepository,
            IDomainEventDispatcher domainEventDispatcher
        )
        {
            _charactersRepository = charactersRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public EnumCharacterDirection Execute(ITurn90DegreesRightCommand command)
        {
            ICharacterEntity characterEntity = _charactersRepository.Find(command._characterId);
            EnumCharacterDirection newDirection = EnumCharacterDirection.LEFT;

            switch (characterEntity.Direction)
            {
                case EnumCharacterDirection.LEFT:
                    newDirection = EnumCharacterDirection.UP;
                    break;
                case EnumCharacterDirection.UP:
                    newDirection = EnumCharacterDirection.RIGHT;
                    break;
                case EnumCharacterDirection.RIGHT:
                    newDirection = EnumCharacterDirection.DOWN;
                    break;
            }
            characterEntity.Rebounce(0.5f);
            characterEntity.UpdateDirection(newDirection);
            _charactersRepository.Update(characterEntity);
            _domainEventDispatcher.Dispatch(characterEntity);

            return characterEntity.Direction;
        }
    }
}
