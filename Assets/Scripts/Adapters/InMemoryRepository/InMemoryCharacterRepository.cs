using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.Characters.Repositories;
using Domain.Characters.Entities;
using Domain.Characters.Exceptions;

public class InMemoryCharacterRepository : ICharactersRepository
{
    private Dictionary<Guid, CharacterEntity> Store;

    public InMemoryCharacterRepository(Dictionary<Guid, CharacterEntity> store)
    {
        this.Store = store;
    }

    public void Add(CharacterEntity character)
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

    public void Remove(CharacterEntity character)
    {
        if (this.Store.ContainsKey(character.Id))
        {
            this.Store.Remove(character.Id);
        } else
        {
            throw new CharacterException.NotFound(character.Id.ToString());
        }
    }
    public CharacterEntity Find(Guid characterId)
    {
        CharacterEntity character;
        bool hasValue = this.Store.TryGetValue(characterId, out character);
        if (hasValue)
        {
            return CharacterEntity.Create(character.Type, character.Direction, character.Position, character.Speed);
        }
        else
        {
            throw new CharacterException.NotFound(character.Id.ToString());
        }
    }
    public IReadOnlyList<CharacterEntity> GetAll()
    {
        Dictionary<Guid, CharacterEntity>.ValueCollection charactersValueCollection = this.Store.Values;
        List<CharacterEntity> charactersList = new List<CharacterEntity>();

        foreach (CharacterEntity character in charactersValueCollection)
        {
            charactersList.Add(character);
        }

        return charactersList.AsReadOnly();
    }
    public IReadOnlyList<CharacterEntity> Where(Expression<Func<CharacterEntity, bool>> predicate)
    {
        // example of predicate: characterEntity => characterEntity.Position.Value.X < 10.0f
        List<CharacterEntity> filteredCharacters = new List<CharacterEntity>(this.GetAll().Where(predicate.Compile()));
        return filteredCharacters;
    }
}
