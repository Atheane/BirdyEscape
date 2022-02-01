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

        public CreateCharacter(ICharactersRepository charactersRepository, IDomainEventDispatcher domainEventDispatcher)
        {
            Debug.Log("new CreateCharacter");
            _charactersRepository = charactersRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }
        public ICharacterEntity Execute(ICreateCharacterCommand command)
        {
            Debug.Log("Execute");
            var position = VOPosition.Create(command.Position);
            var character = CharacterEntity.Create(command.Type, command.Direction, position, command.Speed);

            _charactersRepository.Add(character);
            Debug.Log("_____");
            _domainEventDispatcher.Dispatch(character);
            Debug.Log("Dispatch");
            return character;
        }
    }
}
