using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Domain.Repositories;
using Domain.Types;
using Domain.Entities;
using Usecases.Commands;

namespace Usecases
{
    public class UpdateLevelState : IUsecase<IUpdateLevelStateCommand, EnumLevelState>
    {
        public ILevelsRepository _levelsRepository;
        public IDomainEventDispatcher _domainEventDispatcher;
        public ILevelEntity _levelEntity;

        public UpdateLevelState(
            ILevelsRepository levelsRepository,
            IDomainEventDispatcher domainEventDispatcher
        )
        {
            _levelsRepository = levelsRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public EnumLevelState Execute(IUpdateLevelStateCommand command)
        {
            _levelEntity = _levelsRepository.Find(command._levelId);
            _levelEntity.UpdateState(command._state);
            _levelsRepository.Update(_levelEntity);
            _domainEventDispatcher.Dispatch(_levelEntity);
            return _levelEntity._state;
        }

    }
}



