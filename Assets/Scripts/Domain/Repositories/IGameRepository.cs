using Domain.Entities;

namespace Domain.Repositories
{
    public interface IGameRepository
    {
        void Save(IGameEntity game);
        IGameEntity Load();
    }
}


