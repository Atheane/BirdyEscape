using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.Characters.Repositories;
using Domain.Characters.Entities;
using Domain.Characters.Exceptions;

namespace Adapters.InMemoryRepository
{
    public class InMemoryCharacterRepository : ICharactersRepository
    {
        private Dictionary<Guid, ICharacterEntity> Store;

        public InMemoryCharacterRepository(Dictionary<Guid, ICharacterEntity> store)
        {
            this.Store = store;
        }

        public void Add(ICharacterEntity character)
        {
            if (!this.Store.ContainsKey(character.Id))
            {
                this.Store.Add(character.Id, character);
            }
            else
            {
                throw new CharacterException.AlreadyExists(character.Id.ToString());
            }
        }

        public void Update(ICharacterEntity newCharacter)
        {
            ICharacterEntity oldCharacter = Find(newCharacter.Id);
            if (oldCharacter.Equals(newCharacter))
            {
                throw new CharacterException.PropertiesAreUnchanged(newCharacter.Id.ToString());
            } else
            {
                this.Store[oldCharacter.Id] = newCharacter;
            }
        }

        public void Remove(ICharacterEntity character)
        {
            if (this.Store.ContainsKey(character.Id))
            {
                this.Store.Remove(character.Id);
            }
            else
            {
                throw new CharacterException.NotFound(character.Id.ToString());
            }
        }
        public ICharacterEntity Find(Guid characterId)
        {
            ICharacterEntity character;
            bool hasValue = this.Store.TryGetValue(characterId, out character);
            if (hasValue)
            {
                return character;
            }
            else
            {
                throw new CharacterException.NotFound(characterId.ToString());
            }
        }
        public IReadOnlyList<ICharacterEntity> GetAll()
        {
            Dictionary<Guid, ICharacterEntity>.ValueCollection charactersValueCollection = this.Store.Values;
            List<ICharacterEntity> charactersList = new List<ICharacterEntity>();

            foreach (ICharacterEntity character in charactersValueCollection)
            {
                charactersList.Add(character);
            }

            return charactersList.AsReadOnly();
        }
        public IReadOnlyList<ICharacterEntity> Where(Expression<Func<ICharacterEntity, bool>> predicate)
        {
            // example of predicate: characterEntity => characterEntity.Position.Value.X < 10.0f
            List<ICharacterEntity> filteredCharacters = new List<ICharacterEntity>(this.GetAll().Where(predicate.Compile()));
            return filteredCharacters;
        }
    }
}
