using UnityEngine;
using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Libs.Adapters;
using Usecases.Commands;
using Domain.ValueObjects;
using Domain.Entities;

namespace Usecases
{
    public class CreateTile : IUsecase<ICreateTileCommand, ITileEntity>
    {
        public IDomainEventDispatcher _domainEventDispatcher;
        public IMapper<VOCoordinates, Vector3> _mapper;

        public CreateTile(
            IDomainEventDispatcher domainEventDispatcher,
            IMapper<VOCoordinates, Vector3> mapper
        )
        {
            _domainEventDispatcher = domainEventDispatcher;
            _mapper = mapper;
        }
        public ITileEntity Execute(ICreateTileCommand command)
        {
            var coordinates = _mapper.ToDomain(command._position);
            var tileEntity = TileEntity.Create(coordinates, VOPath.Create(command._path));

            _domainEventDispatcher.Dispatch(tileEntity);
            return tileEntity;
        }
    }
}

