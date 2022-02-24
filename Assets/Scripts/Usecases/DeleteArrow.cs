using UnityEngine;
using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Usecases.Commands;
using Domain.Entities;
using Domain.Repositories;

namespace Usecases
{
    public class DeleteArrow : IUsecase<IDeleteArrowCommand, ITileEntity>
    {
        public IDomainEventDispatcher _domainEventDispatcher;
        public ITilesRepository _tileRepository;

        public DeleteArrow(
            IDomainEventDispatcher domainEventDispatcher,
            ITilesRepository tileRepository
        )
        {
            _domainEventDispatcher = domainEventDispatcher;
            _tileRepository = tileRepository;

        }
        public ITileEntity Execute(IDeleteArrowCommand command)
        {
            ITileEntity tile = _tileRepository.Find(command._tileId);
            tile.RemoveArrow();
            _tileRepository.Update(tile);
            _domainEventDispatcher.Dispatch(tile);
            return tile;
        }
    }
}


