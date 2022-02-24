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
    public class CreateArrow : IUsecase<ICreateArrowCommand, ITileEntity>
    {
        public IDomainEventDispatcher _domainEventDispatcher;
        public IMapper<VOCoordinates, Vector3> _mapper;
        public ITilesRepository _tileRepository;

        public CreateArrow(
            IDomainEventDispatcher domainEventDispatcher,
            ITilesRepository tileRepository,
            IMapper<VOCoordinates, Vector3> mapper
        )
        {
            _domainEventDispatcher = domainEventDispatcher;
            _tileRepository = tileRepository;
            _mapper = mapper;
        }
        public ITileEntity Execute(ICreateArrowCommand command)
        {
            var tileEntity = _tileRepository.Find(command._tileId);

            var coordinates = _mapper.ToDomain(command._position);
            var path = VOPath.Create(command._path);

            var arrowEntity = ArrowEntity.Create(
                command._direction,
                coordinates,
                path);

            tileEntity.AddArrow(arrowEntity);
            _tileRepository.Update(tileEntity);
            _domainEventDispatcher.Dispatch(tileEntity);

            _domainEventDispatcher.Dispatch(arrowEntity);
            return tileEntity;
        }
    }
}
