using Libs.Domain.Repositories;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface ITilesRepository : IRepository<ITileEntity>
    {
        void Add(ITileEntity tile);
        void Update(ITileEntity tile);
        void Remove(ITileEntity tile);
    }
}
