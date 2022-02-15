using UnityEngine;
using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Usecases.Commands;
using Domain.Entities;
using Domain.ValueObjects;
using Domain.Repositories;

namespace Usecases
{
    public class CreateCharacter : IUsecase<ICreateCharacterCommand, ICharacterEntity>
    {
        public ICharactersRepository _charactersRepository;
        public IDomainEventDispatcher _domainEventDispatcher;

        public CreateCharacter(
            ICharactersRepository charactersRepository,
            IDomainEventDispatcher domainEventDispatcher
        )
        {
            _charactersRepository = charactersRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }
        public ICharacterEntity Execute(ICreateCharacterCommand command)
        {
            var position = VOPosition3D.Create(command._position);
            var characterEntity = CharacterEntity.Create(command._type, command._direction, position, command._speed);

            _charactersRepository.Add(characterEntity);

            _domainEventDispatcher.Dispatch(characterEntity);
            return characterEntity;
        }
    }
}
