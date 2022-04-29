using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.Repositories;
using Domain.Entities;
using Domain.Exceptions;

namespace Adapters.InMemoryRepository
{
    public class InMemoryLevelRepository : ILevelsRepository
    {
        private Dictionary<Guid, ILevelEntity> _store;

        public InMemoryLevelRepository(Dictionary<Guid, ILevelEntity> store)
        {
            _store = store;
        }

        public void Add(ILevelEntity level)
        {
            if (!_store.ContainsKey(level._id))
            {
                _store.Add(level._id, level);
            }
            else
            {
                throw new LevelException.AlreadyExists(level._id.ToString());
            }
        }

        public void Update(ILevelEntity newLevel)
        {
            ILevelEntity oldLevel = Find(newLevel._id);
            //todo: test equality on each prop and return and error if objet is unchanged
            _store[oldLevel._id] = newLevel;
        }

        public ILevelEntity Find(Guid levelId)
        {
            ILevelEntity level;
            bool hasValue = _store.TryGetValue(levelId, out level);
            if (hasValue)
            {
                return level;
            }
            else
            {
                throw new LevelException.NotFound(levelId.ToString());
            }
        }

        public IReadOnlyList<ILevelEntity> GetAll()
        {
            Dictionary<Guid, ILevelEntity>.ValueCollection levelsValueCollection = _store.Values;
            List<ILevelEntity> levelsList = new List<ILevelEntity>();

            foreach (ILevelEntity level in levelsValueCollection)
            {
                levelsList.Add(level);
            }

            return levelsList.AsReadOnly();
        }

        public IReadOnlyList<ILevelEntity> Where(Expression<Func<ILevelEntity, bool>> predicate)
        {
            List<ILevelEntity> filteredLevels = new List<ILevelEntity>(GetAll().Where(predicate.Compile()));
            return filteredLevels;
        }
    }
}


