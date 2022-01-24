using System;
using System.Collections.Generic;
using UnityEngine;
using UniMediator;
using Domain.Characters.Entities;
using Domain.Characters.Types;
using Usecases.Characters;
using Adapters.InMemoryRepository;
using Frameworks.Messages;

public class MoveCharacterByIdHandler : MonoBehaviour,
ISingleMessageHandler<MoveCharacterByIdMessage, EnumCharacterState>
{
    public EnumCharacterState Handle(MoveCharacterByIdMessage message)
    {
        // invoke adapter handler
        // should be the singleton repo, if different, won't find characterId
        var repository = new InMemoryCharacterRepository(new Dictionary<Guid, CharacterEntity>());
        var characterState = new MoveCharacterById(repository).Execute(message);

        // Map Character entty to DTO (with image source)
        this.MoveCharacter();
        return characterState;
    }
    public void MoveCharacter()
    {
        Transform grid = this.transform;
        GameObject cow = this.transform.gameObject;
        cow.transform.parent = grid;
    }
}