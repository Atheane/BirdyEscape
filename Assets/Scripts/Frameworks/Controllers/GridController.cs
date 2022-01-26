using UnityEngine;
using Zenject;
using Domain.Characters.Types;
using Domain.Characters.Constants;
using Usecases.Characters;
using Usecases.Characters.Commands;
using Frameworks.Dtos;


public class GridController : MonoBehaviour
{
    private DiContainer _container;
    private GameObject _cowGo;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    void Awake()
    {
        Debug.Log("________GridController, Start()");
        var character = _container.Resolve<CreateCharacter>().Execute(new CreateCharacterCommand(EnumCharacterType.COW, EnumCharacterDirection.LEFT, (Position.INIT_X, Position.INIT_Y), Speed.INIT_SPEED));
        Debug.Log("Created cow");
        Debug.Log(character);
        var _character = CharacterDto.Create(character.Id, character.Type, character.Direction, new Vector3(character.Position.Value.X, character.Position.Value.Y), character.Speed);
        DrawCharacter(_character);
    }

    public void DrawCharacter(ICharacterDto characterDto)
    {
        Transform grid = transform;
        _cowGo = Instantiate(Resources.Load(characterDto.Image), characterDto.Position, Quaternion.identity) as GameObject;
        // instantiate and attach the component in once function
        _container.InstantiateComponent<CharacterMoveController>(_cowGo);
        _cowGo.tag = characterDto.Image;
        _cowGo.transform.parent = grid;
    }

}

