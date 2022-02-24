using Libs.Domain.Repositories;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IArrowsRepository : IRepository<IArrowEntity>
    {
        void Add(IArrowEntity arrow);
        void Update(IArrowEntity arrow);
        void Remove(IArrowEntity arrow);
    }
}

