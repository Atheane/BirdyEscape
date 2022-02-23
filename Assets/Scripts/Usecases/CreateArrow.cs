using UnityEngine;
using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Libs.Adapters;
using Usecases.Commands;
using Domain.ValueObjects;
using Domain.Entities;
using Domain.Repositories;

namespace Usecases
{
    public class CreateArrow : IUsecase<ICreateArrowCommand, IArrowEntity>
    {
        public IDomainEventDispatcher _domainEventDispatcher;
        public IMapper<VOCoordinates, Vector3> _mapper;
        public IArrowsRepository _arrowRepository;

        public CreateArrow(
            IDomainEventDispatcher domainEventDispatcher,
            IArrowsRepository arrowRepository,
            IMapper<VOCoordinates, Vector3> mapper
        )
        {
            _domainEventDispatcher = domainEventDispatcher;
            _arrowRepository = arrowRepository;
            _mapper = mapper;
        }
        public IArrowEntity Execute(ICreateArrowCommand command)
        {
            var coordinates = _mapper.ToDomain(command._position);
            var path = VOPath.Create(command._path);

            var arrowEntity = ArrowEntity.Create(command._direction, coordinates, path);
            _arrowRepository.Add(arrowEntity);
            _domainEventDispatcher.Dispatch(arrowEntity);
            return arrowEntity;
        }
    }
}
