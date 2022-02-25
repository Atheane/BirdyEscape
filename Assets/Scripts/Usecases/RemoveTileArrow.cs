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
        public ITilesRepository _tileRepository;
        public IDomainEventDispatcher _domainEventDispatcher;

        public RemoveTileArrow(
            ITilesRepository tileRepository,
            IDomainEventDispatcher domainEventDispatcher
        )
        {
            _tileRepository = tileRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public ITileEntity Execute(IRemoveTileArrowCommand command)
        {
            ITileEntity tile = _tileRepository.Find(command._tileId);
            tile.RemoveArrow();
            _tileRepository.Update(tile);
            _domainEventDispatcher.Dispatch(tile);
            return tile;
        }
    }
}


