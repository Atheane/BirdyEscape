using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.Repositories;
using Domain.Entities;
using Domain.Exceptions;

namespace Adapters.InMemoryRepository
{
    public class InMemoryArrowRepository : IArrowsRepository
    {
        private Dictionary<Guid, IArrowEntity> _store;

        public InMemoryArrowRepository(Dictionary<Guid, IArrowEntity> store)
        {
            _store = store;
        }

        public void Add(IArrowEntity arrow)
        {
            if (!_store.ContainsKey(arrow._id))
            {
                _store.Add(arrow._id, arrow);
            }
            else
            {
                throw new CharacterException.AlreadyExists(arrow._id.ToString());
            }
        }

        public void Remove(IArrowEntity arrow)
        {
            if (_store.ContainsKey(arrow._id))
            {
                _store.Remove(arrow._id);
            }
            else
            {
                throw new CharacterException.NotFound(arrow._id.ToString());
            }
        }

        public IArrowEntity Find(Guid arrowId)
        {
            IArrowEntity arrow;
            bool hasValue = _store.TryGetValue(arrowId, out arrow);
            if (hasValue)
            {
                return arrow;
            }
            else
            {
                throw new CharacterException.NotFound(arrowId.ToString());
            }
        }

        public IReadOnlyList<IArrowEntity> GetAll()
        {
            Dictionary<Guid, IArrowEntity>.ValueCollection arrowsValueCollection = _store.Values;
            List<IArrowEntity> arrowsList = new List<IArrowEntity>();

            foreach (IArrowEntity arrow in arrowsValueCollection)
            {
                arrowsList.Add(arrow);
            }

            return arrowsList.AsReadOnly();
        }

        public IReadOnlyList<IArrowEntity> Where(Expression<Func<IArrowEntity, bool>> predicate)
        {
            List<IArrowEntity> filteredArrows = new List<IArrowEntity>(GetAll().Where(predicate.Compile()));
            return filteredArrows;
        }
    }
}

