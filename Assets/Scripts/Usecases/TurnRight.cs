using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Usecases.Commands;
using Domain.Repositories;
using Domain.Types;
using Domain.Entities;


namespace Usecases
{
    public class TurnRight : IUsecase<ITurnRightCommand, EnumDirection>
    {
        public ICharactersRepository _charactersRepository;
        public IDomainEventDispatcher _domainEventDispatcher;

        public TurnRight(
            ICharactersRepository charactersRepository,
            IDomainEventDispatcher domainEventDispatcher
        )
        {
            _charactersRepository = charactersRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public EnumDirection Execute(ITurnRightCommand command)
        {
            ICharacterEntity characterEntity = _charactersRepository.Find(command._characterId);
            EnumDirection newDirection = EnumDirection.LEFT;

            switch (characterEntity.Direction)
            {
                case EnumDirection.LEFT:
                    newDirection = EnumDirection.UP;
                    break;
                case EnumDirection.UP:
                    newDirection = EnumDirection.RIGHT;
                    break;
                case EnumDirection.RIGHT:
                    newDirection = EnumDirection.DOWN;
                    break;
            }
            characterEntity.UpdateDirection(newDirection);

            _charactersRepository.Update(characterEntity);
            _domainEventDispatcher.Dispatch(characterEntity);

            return characterEntity.Direction;
        }
    }
}
