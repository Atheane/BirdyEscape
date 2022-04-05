using Libs.Domain.Repositories;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IGameRepository : IRepository<IGameEntity>
    {
        void Add(IGameEntity game);
        void Update(IGameEntity game);
    }
}


