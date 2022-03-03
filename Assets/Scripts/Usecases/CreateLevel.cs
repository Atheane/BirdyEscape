using UnityEngine;
using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Usecases.Commands;
using Domain.Entities;
using Domain.Repositories;

namespace Usecases
{
    public class CreateLevel : IUsecase<ICreateLevelCommand, ILevelEntity>
    {
        public ILevelsRepository _levelsRepository;
        public IDomainEventDispatcher _domainEventDispatcher;

        public CreateLevel(
            ILevelsRepository levelsRepository,
            IDomainEventDispatcher domainEventDispatcher
        )
        {
            _levelsRepository = levelsRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }
        public ILevelEntity Execute(ICreateLevelCommand command)
        {
            var levelEntity = LevelEntity.Create(command._number, command._characters.ToArray(), command._state);
            _levelsRepository.Add(levelEntity);
            _domainEventDispatcher.Dispatch(levelEntity);
            return levelEntity;
        }
    }
}
