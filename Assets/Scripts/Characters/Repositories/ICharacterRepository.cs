using Base.Repositories;
using Characters.Entities;

namespace Character.Repositories
{
    public interface ICharactersRepository : IRepository<CharacterEntity>
    {
        void Add(CharacterEntity character);

        void Remove(CharacterEntity character);
    }
}

