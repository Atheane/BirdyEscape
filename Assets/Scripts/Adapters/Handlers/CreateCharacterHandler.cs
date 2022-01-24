using System;
using System.Collections.Generic;
using UnityEngine;
using UniMediator;
using Domain.Characters.Entities;
using Usecases.Characters;
using Adapters.InMemoryRepository;
using Frameworks.Messages;
using Frameworks.Dtos;

public class CreateCharacterHandler : MonoBehaviour,
ISingleMessageHandler<CreateCharacterMessage, ICharacterDto>
{
    public ICharacterDto Handle(CreateCharacterMessage message)
    {
        // invoke adapter handler
        var repository = new InMemoryCharacterRepository(new Dictionary<Guid, CharacterEntity>());
        var character = new CreateCharacter(repository).Execute(message);

        // Map Character entty to DTO (with image source)
        var characterDto = CharacterDto.Create(character.Id, character.Type, character.Direction, new Vector3(character.Position.Value.X, character.Position.Value.Y), character.Speed);
        this.DrawCharacter(characterDto);
        return characterDto;
    }
    public void DrawCharacter(ICharacterDto characterDto)
    {
        Transform grid = this.transform;
        GameObject cow = Instantiate(Resources.Load(characterDto.Image), characterDto.Position, Quaternion.identity) as GameObject;
        cow.transform.parent = grid;
    }
}