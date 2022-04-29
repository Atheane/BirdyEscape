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
    public class AddTileArrow : IUsecase<IAddTileArrowCommand, ITileEntity>
    {
        public IDomainEventDispatcher _domainEventDispatcher;
        public IMapper<VOCoordinates, Vector3> _mapper;
        public ITilesRepository _tileRepository;

        public AddTileArrow(
            IDomainEventDispatcher domainEventDispatcher,
            ITilesRepository tileRepository,
            IMapper<VOCoordinates, Vector3> mapper
        )
        {
            _domainEventDispatcher = domainEventDispatcher;
            _tileRepository = tileRepository;
            _mapper = mapper;
        }
        public ITileEntity Execute(IAddTileArrowCommand command)
        {
            var tileEntity = _tileRepository.Find(command._tileId);
            var path = VOPath.Create(command._path);


            tileEntity.AddArrow(command._direction, tileEntity._coordinates, path);
            _tileRepository.Update(tileEntity);
            _domainEventDispatcher.Dispatch(tileEntity);

            return tileEntity;
        }
    }
}
