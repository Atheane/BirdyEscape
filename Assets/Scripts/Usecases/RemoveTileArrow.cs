using UnityEngine;
using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Usecases.Commands;
using Domain.Entities;
using Domain.Repositories;

namespace Usecases
{
    public class RemoveTileArrow : IUsecase<IRemoveTileArrowCommand, ITileEntity>
    {
        public IDomainEventDispatcher _domainEventDispatcher;
        public ITilesRepository _tileRepository;

        public RemoveTileArrow(
            IDomainEventDispatcher domainEventDispatcher,
            ITilesRepository tileRepository
        )
        {
            _domainEventDispatcher = domainEventDispatcher;
            _tileRepository = tileRepository;

        }
        public ITileEntity Execute(IRemoveTileArrowCommand command)
        {
            ITileEntity tile = _tileRepository.Find(command._tileId);
            tile.RemoveArrow();
            _domainEventDispatcher.Dispatch(tile._arrow);

            _tileRepository.Update(tile);
            return tile;
        }
    }
}


