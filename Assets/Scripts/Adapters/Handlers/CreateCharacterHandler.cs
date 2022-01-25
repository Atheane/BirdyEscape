using UnityEngine;
using UniMediator;
using Zenject;
using Usecases.Characters;
using Frameworks.Messages;
using Frameworks.Dtos;

public class CreateCharacterHandler : MonoBehaviour,
ISingleMessageHandler<CreateCharacterMessage, ICharacterDto>
{
    private CreateCharacter _usecase;

    [Inject]
    public void Construct(CreateCharacter usecase)
    {
        _usecase = usecase;
    }

    public ICharacterDto Handle(CreateCharacterMessage message)
    {
        var character = _usecase.Execute(message);
        var characterDto = CharacterDto.Create(character.Id, character.Type, character.Direction, new Vector3(character.Position.Value.X, character.Position.Value.Y), character.Speed);
        DrawCharacter(characterDto);
        return characterDto;
    }

    public void DrawCharacter(ICharacterDto characterDto)
    {
        Transform grid = this.transform;
        GameObject cow = Instantiate(Resources.Load(characterDto.Image), characterDto.Position, Quaternion.identity) as GameObject;
        cow.tag = characterDto.Image;
        cow.transform.parent = grid;
    }
}