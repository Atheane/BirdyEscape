using UnityEngine;
using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Libs.Adapters;
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
        public IMapper<VOPosition, Vector3> _mapper;

        public CreateCharacter(
            ICharactersRepository charactersRepository,
            IDomainEventDispatcher domainEventDispatcher,
            IMapper<VOPosition, Vector3> mapper
        )
        {
            _charactersRepository = charactersRepository;
            _domainEventDispatcher = domainEventDispatcher;
            _mapper = mapper;
        }
        public ICharacterEntity Execute(ICreateCharacterCommand command)
        {
            var position = _mapper.ToDomain(command._position);
            var characterEntity = CharacterEntity.Create(command._type, command._direction, position, command._speed);

            _charactersRepository.Add(characterEntity);

            _domainEventDispatcher.Dispatch(characterEntity);
            return characterEntity;
        }
    }
}
