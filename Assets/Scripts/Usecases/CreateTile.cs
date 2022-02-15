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
            var coordinates = VOPositionGrid.ConvertToCoordinates(command._position);
            var position = VOPositionGrid.Create(coordinates);
            var image = VOImage.Create(command._image);
            var tileEntity = TileEntity.Create(position, image);

            _domainEventDispatcher.Dispatch(tileEntity);
            return tileEntity;
        }
    }
}

