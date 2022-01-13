using Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Base.Repositories
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IReadOnlyList<T> GetAll();

        T Find(Guid id);

        IReadOnlyList<T> Where(Expression<Func<T, bool>> predicate);
    }
}
