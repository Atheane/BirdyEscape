using UnityEngine;
using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Usecases.Commands;
using Domain.Entities;
using Domain.Repositories;

namespace Usecases
{
    public class DeleteArrow : IUsecase<IDeleteArrowCommand, IArrowEntity>
    {
        public IDomainEventDispatcher _domainEventDispatcher;
        public IArrowsRepository _arrowRepository;

        public DeleteArrow(
            IDomainEventDispatcher domainEventDispatcher,
            IArrowsRepository arrowRepository
        )
        {
            _domainEventDispatcher = domainEventDispatcher;
            _arrowRepository = arrowRepository;

        }
        public IArrowEntity Execute(IDeleteArrowCommand command)
        {
            IArrowEntity arrowEntity = _arrowRepository.Find(command._arrowId);
            arrowEntity.Delete();
            _arrowRepository.Remove(arrowEntity);
            _domainEventDispatcher.Dispatch(arrowEntity);
            return arrowEntity;
        }
    }
}


