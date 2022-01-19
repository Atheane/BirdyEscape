using UnityEngine;
using UniMediator;
using Frameworks.Messages;
using Frameworks.Dtos;
using Adapters.Handlers;
using Usecases.Characters;

public class CreateCharacterHandler : MonoBehaviour,
ISingleMessageHandler<CreateCharacterMessage, ICharacterDto>
{
    public ICharacterDto Handle(CreateCharacterMessage message)
    {
        Debug.Log(">>> CreateCharacterHandler");
        Debug.Log(message);
        // invoke adapter handler
        var usecase = new CreateCharacter();
        var handler = new OnCreateCharacterHandler(usecase);
        var character = handler.Handle(message);

        // Map Character entty to DTO (with image source)
        var characterDto = CharacterDto.Create(character.Id, character.Type, character.Direction, (character.Position.Value.X, character.Position.Value.Y), character.Speed);
        Debug.Log(">>> should return DTO");
        Debug.Log(characterDto);

        return characterDto;
    }
}


