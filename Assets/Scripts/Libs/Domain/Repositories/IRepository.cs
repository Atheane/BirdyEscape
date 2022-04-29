using Libs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Libs.Domain.Repositories
{
    public interface IRepository<T> where T : IEntity, IAggregateRoot
    {
        IReadOnlyList<T> GetAll();

        T Find(Guid id);

        IReadOnlyList<T> Where(Expression<Func<T, bool>> predicate);
    }
}
