using UnityEngine;
using UniMediator;
using Frameworks.Messages;
using Frameworks.Dtos;
using Adapters.Handlers;
using Usecases.Characters;
using Libs.Helpers;

public class CreateCharacterHandler : MonoBehaviour,
ISingleMessageHandler<CreateCharacterMessage, ICharacterDto>
{
    public ICharacterDto Handle(CreateCharacterMessage message)
    {
        // invoke adapter handler
        var usecase = new CreateCharacter();
        var handler = new OnCreateCharacterHandler(usecase);
        var character = handler.Handle(message);

        // Map Character entty to DTO (with image source)
        var characterDto = CharacterDto.Create(character.Id, character.Type, character.Direction, (character.Position.Value.X, character.Position.Value.Y), character.Speed);
        this.DrawCharacter(characterDto);
        return characterDto;
    }
    public void DrawCharacter(ICharacterDto characterDto)
    {
        Debug.Log("_________________________DrawCharacter");
        string src = "Assets/Prefabs/Character/" + StringExtensions.FirstCharToUpper(characterDto.Image);
        Debug.Log(src);
        GameObject cow = GameObject.Instantiate(Resources.Load(src)) as GameObject;
        Debug.Log(cow);
    }
}