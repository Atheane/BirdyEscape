using UnityEngine;
using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Usecases.Commands;
using Domain.ValueObjects;
using Domain.Entities;

namespace Usecases
{
    public class CreateTile : IUsecase<ICreateTileCommand, ITileEntity>
    {
        public IDomainEventDispatcher _domainEventDispatcher;

        public CreateTile(
            IDomainEventDispatcher domainEventDispatcher
        )
        {
            _domainEventDispatcher = domainEventDispatcher;
        }
        public ITileEntity Execute(ICreateTileCommand command)
        {
            var coordinates = VOCoordinates.ConvertToCoordinates(command._position);
            var position = VOCoordinates.Create(coordinates);
            var image = VOPath.Create(command._path);
            var tileEntity = TileEntity.Create(position, image);

            _domainEventDispatcher.Dispatch(tileEntity);
            return tileEntity;
        }
    }
}

