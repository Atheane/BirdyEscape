using UnityEngine;
using UniMediator;
using Zenject;
using System;
using Usecases.Characters;
using Frameworks.Messages;
using Frameworks.Dtos;

public class CreateCharacterHandler : MonoBehaviour,
ISingleMessageHandler<CreateCharacterMessage, ICharacterDto>
{
    private CreateCharacter _usecase;
    private static GameObject _cow;
    private static Guid _id;

    [Inject]
    DiContainer _container;

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
        _id = characterDto.Id;
        return characterDto;
    }

    public void DrawCharacter(ICharacterDto characterDto)
    {
        Transform grid = transform;
        _cow = Instantiate(Resources.Load(characterDto.Image), characterDto.Position, Quaternion.identity) as GameObject;
        // instantiate and attach the component in once function 
        _container.InstantiateComponent<CharacterMoveController>(_cow);
        _cow.tag = characterDto.Image;
        _cow.transform.parent = grid;

    }

    public static Guid GetCharacterId()
    {
        return _id;
    }

    public static GameObject GetGoInstance()
    {
        return _cow;
    }

}