using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Usecases.Commands;
using Domain.Repositories;
using Domain.Entities;


namespace Usecases
{
    public class UpdateArrowDirection : IUsecase<IUpdateArrowDirectionCommand, ITileEntity>
    {
        public ITilesRepository _tilesRepository;
        public IDomainEventDispatcher _domainEventDispatcher;

        public UpdateArrowDirection(
            ITilesRepository tilesRepository,
            IDomainEventDispatcher domainEventDispatcher
        )
        {
            _tilesRepository = tilesRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public ITileEntity Execute(IUpdateArrowDirectionCommand command)
        {
            ITileEntity tile = _tilesRepository.Find(command._tileId);
            tile.UpdateArrowDirection(command._direction);
            _tilesRepository.Update(tile);
            _domainEventDispatcher.Dispatch(tile);
            return tile;
        }
    }
}
