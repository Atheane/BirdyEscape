using UnityEngine;
using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Usecases.Commands;
using Domain.ValueObjects;
using Domain.Entities;

namespace Usecases
{
    public class CreateArrow : IUsecase<ICreateArrowCommand, IArrowEntity>
    {
        public IDomainEventDispatcher _domainEventDispatcher;

        public CreateArrow(
            IDomainEventDispatcher domainEventDispatcher
        )
        {
            _domainEventDispatcher = domainEventDispatcher;
        }
        public IArrowEntity Execute(ICreateArrowCommand command)
        {
            var coords = VOCoordinates.Create(command._coordinates);
            var path = VOPath.Create(command._path);

            var arrowEntity = ArrowEntity.Create(command._direction, coords, path);

            _domainEventDispatcher.Dispatch(arrowEntity);
            return arrowEntity;
        }
    }
}
