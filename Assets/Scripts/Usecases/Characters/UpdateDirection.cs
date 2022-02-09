using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Usecases.Characters.Commands;
using Domain.Characters.Repositories;
using Domain.Commons.Types;
using Domain.Characters.Entities;


namespace Usecases.Characters
{
    public class UpdateDirection : IUsecase<IUpdateDirectionCommand, EnumDirection>
    {
        public ICharactersRepository _charactersRepository;
        public IDomainEventDispatcher _domainEventDispatcher;

        public UpdateDirection(
            ICharactersRepository charactersRepository,
            IDomainEventDispatcher domainEventDispatcher
        )
        {
            _charactersRepository = charactersRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public EnumDirection Execute(IUpdateDirectionCommand command)
        {
            ICharacterEntity characterEntity = _charactersRepository.Find(command._characterId);

            characterEntity.UpdateDirection(command._direction);

            _charactersRepository.Update(characterEntity);
            _domainEventDispatcher.Dispatch(characterEntity);

            return characterEntity.Direction;
        }
    }
}
