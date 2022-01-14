using Libs.Domain.Repositories;
using Domain.Characters.Entities;

namespace Domain.Characters.Repositories
{
    public interface ICharactersRepository : IRepository<CharacterEntity>
    {
        void Add(CharacterEntity character);

        void Remove(CharacterEntity character);
    }
}

