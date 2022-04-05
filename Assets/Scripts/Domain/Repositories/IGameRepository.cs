using Libs.Domain.Repositories;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IGameRepository : IRepository<IGameEntity>
    {
        void Save(IGameEntity game);
        IGameEntity Load();
    }
}


