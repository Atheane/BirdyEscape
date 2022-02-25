using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.Repositories;
using Domain.Entities;
using Domain.Exceptions;

namespace Adapters.InMemoryRepository
{
    public class InMemoryTileRepository : ITilesRepository
    {
        private Dictionary<Guid, ITileEntity> _store;

        public InMemoryTileRepository(Dictionary<Guid, ITileEntity> store)
        {
            _store = store;
        }

        public void Add(ITileEntity tile)
        {
            if (!_store.ContainsKey(tile._id))
            {
                _store.Add(tile._id, tile);
            }
            else
            {
                throw new TileException.AlreadyExists(tile._id.ToString());
            }
        }

        public void Update(ITileEntity newTile)
        {
            ITileEntity oldTile = Find(newTile._id);
            //todo: test equality on each prop and return and error if objet is unchanged
            _store[oldTile._id] = newTile;
        }

        public void Remove(ITileEntity tile)
        {
            if (_store.ContainsKey(tile._id))
            {
                _store.Remove(tile._id);
            }
            else
            {
                throw new TileException.NotFound(tile._id.ToString());
            }
        }

        public ITileEntity Find(Guid tileId)
        {
            ITileEntity tile;
            bool hasValue = _store.TryGetValue(tileId, out tile);
            if (hasValue)
            {
                return tile;
            }
            else
            {
                throw new TileException.NotFound(tileId.ToString());
            }
        }

        public IReadOnlyList<ITileEntity> GetAll()
        {
            Dictionary<Guid, ITileEntity>.ValueCollection tilesValueCollection = _store.Values;
            List<ITileEntity> tilesList = new List<ITileEntity>();

            foreach (ITileEntity tile in tilesValueCollection)
            {
                tilesList.Add(tile);
            }

            return tilesList.AsReadOnly();
        }

        public IReadOnlyList<ITileEntity> Where(Expression<Func<ITileEntity, bool>> predicate)
        {
            List<ITileEntity> filteredTiles = new List<ITileEntity>(GetAll().Where(predicate.Compile()));
            return filteredTiles;
        }
    }
}

