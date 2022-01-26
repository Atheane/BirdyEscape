using Libs.Domain.Repositories;
using Domain.Characters.Entities;

namespace Domain.Characters.Repositories
{
    public interface ICharactersRepository : IRepository<ICharacterEntity>
    {
        void Add(ICharacterEntity character);
        void Update(ICharacterEntity character);
        void Remove(ICharacterEntity character);
    }
}

