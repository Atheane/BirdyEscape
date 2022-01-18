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
        Debug.Log(">>> IN MESSAGE");
        Debug.Log(message);
        // invoke adapter handler
        var usecase = new CreateCharacter();
        var handler = new OnCreateCharacterHandler(usecase);
        // map message to command
        var command = new CharacterMapper().ToDomain(message);
        // retrieve character entity with dapater handler
        var character = handler.Handle(command);
        // Map Character entty to DTO (with image source)
        var characterDto = CharacterDto.Create(character.Id, character.Type, character.Direction, (character.Position.Value.X, character.Position.Value.Y), character.Speed);

        return characterDto;
    }
}


