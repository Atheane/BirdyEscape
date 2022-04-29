using Libs.Domain.Repositories;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface ILevelsRepository : IRepository<ILevelEntity>
    {
        void Add(ILevelEntity tile);
        void Update(ILevelEntity tile);
    }
}

