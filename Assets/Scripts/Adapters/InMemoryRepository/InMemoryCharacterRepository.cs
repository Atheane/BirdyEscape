using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.Repositories;
using Domain.Entities;
using Domain.Exceptions;

namespace Adapters.InMemoryRepository
{
    public class InMemoryCharacterRepository : ICharactersRepository
    {
        private Dictionary<Guid, ICharacterEntity> _store;

        public InMemoryCharacterRepository(Dictionary<Guid, ICharacterEntity> store)
        {
            _store = store;
        }

        public void Add(ICharacterEntity character)
        {
            if (!_store.ContainsKey(character._id))
            {
                _store.Add(character._id, character);
                Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                Debug.Log(character._id);
                Debug.Log(_store.Values.First<ICharacterEntity>()._id);
            }
            else
            {
                throw new CharacterException.AlreadyExists(character._id.ToString());
            }
        }

        public void Update(ICharacterEntity newCharacter)
        {
            ICharacterEntity oldCharacter = Find(newCharacter._id);
            //todo: test equality on each prop and return and error if objet is unchanged
            _store[oldCharacter._id] = newCharacter;
        }

        public void Remove(ICharacterEntity character)
        {
            if (_store.ContainsKey(character._id))
            {
                _store.Remove(character._id);
            }
            else
            {
                throw new CharacterException.NotFound(character._id.ToString());
            }
        }

        public ICharacterEntity Find(Guid characterId)
        {
            ICharacterEntity character;
            bool hasValue = _store.TryGetValue(characterId, out character);
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
            Dictionary<Guid, ICharacterEntity>.ValueCollection charactersValueCollection = _store.Values;
            List<ICharacterEntity> charactersList = new List<ICharacterEntity>();
            Debug.Log("<<<<<<<<<<<<<<<<<<<<<<<");
            Debug.Log(_store.Values.First<ICharacterEntity>()._id);

            foreach (ICharacterEntity character in charactersValueCollection)
            {
                Debug.Log(character._id);
                charactersList.Add(character);
            }

            return charactersList.AsReadOnly();
        }

        public IReadOnlyList<ICharacterEntity> Where(Expression<Func<ICharacterEntity, bool>> predicate)
        {
            // example of predicate: characterEntity => characterEntity.Position.Value.X < 10.0f
            List<ICharacterEntity> filteredCharacters = new List<ICharacterEntity>(GetAll().Where(predicate.Compile()));
            return filteredCharacters;
        }
    }
}
