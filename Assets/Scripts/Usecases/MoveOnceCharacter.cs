using System;
using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Usecases.Commands;
using Domain.Repositories;
using Domain.ValueObjects;
using Domain.Types;
using Domain.Entities;


namespace Usecases
{
    public class MoveOnceCharacter : IUsecase<IMoveAlwaysCharacterCommand, VOPosition>
    {
        public ICharactersRepository _charactersRepository;
        public IDomainEventDispatcher _domainEventDispatcher;
        public ICharacterEntity _characterEntity;

        public MoveOnceCharacter(
            ICharactersRepository charactersRepository,
            IDomainEventDispatcher domainEventDispatcher
        )
        {
            _charactersRepository = charactersRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public VOPosition Execute(IMoveAlwaysCharacterCommand command)
        {
            _characterEntity = _charactersRepository.Find(command._characterId);
            _characterEntity.MoveOnce();
            _charactersRepository.Update(_characterEntity);
            _domainEventDispatcher.Dispatch(_characterEntity);
            return _characterEntity._position;
        }
    }
}

