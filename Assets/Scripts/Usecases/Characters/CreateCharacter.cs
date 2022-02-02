using UnityEngine;
using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Usecases.Characters.Commands;
using Domain.Characters.Entities;
using Domain.Characters.ValueObjects;
using Domain.Characters.Repositories;

namespace Usecases.Characters
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
            var position = VOPosition.Create(command.Position);
            var characterEntity = CharacterEntity.Create(command.Type, command.Direction, position, command.Speed);

            _charactersRepository.Add(characterEntity);
            _domainEventDispatcher.Dispatch(characterEntity);
            characterEntity.ClearDomainEvents();
            return characterEntity;
        }
    }
}
