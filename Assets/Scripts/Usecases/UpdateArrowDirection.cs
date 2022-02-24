using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Usecases.Commands;
using Domain.Repositories;
using Domain.Entities;


namespace Usecases
{
    public class UpdateArrowDirection : IUsecase<IUpdateArrowDirectionCommand, IArrowEntity>
    {
        public IArrowsRepository _arrowsRepository;
        public IDomainEventDispatcher _domainEventDispatcher;

        public UpdateArrowDirection(
            IArrowsRepository arrowsRepository,
            IDomainEventDispatcher domainEventDispatcher
        )
        {
            _arrowsRepository = arrowsRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public IArrowEntity Execute(IUpdateArrowDirectionCommand command)
        {
            IArrowEntity _arrowEntity = _arrowsRepository.Find(command._arrowId);
            _arrowEntity.UpdateDirection(command._direction);
            _arrowsRepository.Update(_arrowEntity);
            _domainEventDispatcher.Dispatch(_arrowEntity);
            return _arrowEntity;
        }
    }
}
