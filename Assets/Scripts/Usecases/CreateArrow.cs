using UnityEngine;
using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Libs.Adapters;
using Usecases.Commands;
using Domain.ValueObjects;
using Domain.Entities;

namespace Usecases
{
    public class CreateArrow : IUsecase<ICreateArrowCommand, IArrowEntity>
    {
        public IDomainEventDispatcher _domainEventDispatcher;
        public IMapper<VOCoordinates, Vector3> _mapper;

        public CreateArrow(
            IDomainEventDispatcher domainEventDispatcher,
            IMapper<VOCoordinates, Vector3> mapper
        )
        {
            _domainEventDispatcher = domainEventDispatcher;
            _mapper = mapper;
        }
        public IArrowEntity Execute(ICreateArrowCommand command)
        {
            var coordinates = _mapper.ToDomain(command._position);
            var path = VOPath.Create(command._path);

            var arrowEntity = ArrowEntity.Create(command._direction, coordinates, path);

            _domainEventDispatcher.Dispatch(arrowEntity);
            return arrowEntity;
        }
    }
}
