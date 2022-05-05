using Libs.Usecases;
using Libs.Domain.DomainEvents;
using Usecases.Commands;
using Domain.Repositories;
using Domain.Entities;


namespace Usecases
{
    public class UpdateTileArrowEffect : IUsecase<IUpdateTileArrowEffectCommand, ITileEntity>
    {
        public ITilesRepository _tilesRepository;
        public IDomainEventDispatcher _domainEventDispatcher;

        public UpdateTileArrowEffect(
            ITilesRepository tilesRepository,
            IDomainEventDispatcher domainEventDispatcher
        )
        {
            _tilesRepository = tilesRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public ITileEntity Execute(IUpdateTileArrowEffectCommand command)
        {
            ITileEntity tile = _tilesRepository.Find(command._tileId);
            tile.UpdateArrowEffect(command._numberEffects);
            _tilesRepository.Update(tile);
            _domainEventDispatcher.Dispatch(tile);
            return tile;
        }
    }
}

