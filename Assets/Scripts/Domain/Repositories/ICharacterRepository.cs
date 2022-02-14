using Libs.Domain.Repositories;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICharactersRepository : IRepository<ICharacterEntity>
    {
        void Add(ICharacterEntity character);
        void Update(ICharacterEntity character);
        void Remove(ICharacterEntity character);
    }
}

