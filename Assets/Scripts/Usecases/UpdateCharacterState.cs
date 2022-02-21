using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Usecases.Commands;
using Domain.Repositories;
using Domain.Types;
using Domain.Entities;


namespace Usecases
{
    public class UpdateCharacterState : IUsecase<IUpdateCharacterStateCommand, EnumCharacterState>
    {
        public ICharactersRepository _charactersRepository;
        public IDomainEventDispatcher _domainEventDispatcher;
        public ICharacterEntity _characterEntity;

        public UpdateCharacterState(
            ICharactersRepository charactersRepository,
            IDomainEventDispatcher domainEventDispatcher
        )
        {
            _charactersRepository = charactersRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public EnumCharacterState Execute(IUpdateCharacterStateCommand command)
        {
            _characterEntity = _charactersRepository.Find(command._characterId);
            _characterEntity.UpdateState(EnumCharacterState.MOVING);
            _charactersRepository.Update(_characterEntity);
            _domainEventDispatcher.Dispatch(_characterEntity);
            return _characterEntity._state;
        }

    }
}


