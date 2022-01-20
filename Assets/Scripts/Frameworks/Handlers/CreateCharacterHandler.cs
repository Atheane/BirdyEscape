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
        // invoke adapter handler
        var usecase = new CreateCharacter();
        var handler = new OnCreateCharacterHandler(usecase);
        var character = handler.Handle(message);

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