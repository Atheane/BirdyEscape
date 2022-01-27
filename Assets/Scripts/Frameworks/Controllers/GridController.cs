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
    private GameObject _currentGo;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    void Start()
    {
        var characterEntity = _container.Resolve<CreateCharacter>().Execute(new CreateCharacterCommand(EnumCharacterType.COW, EnumCharacterDirection.LEFT, (Position.INIT_X, Position.INIT_Y), Speed.INIT_SPEED));
        Debug.Log("Created cow");
        var characterDto = CharacterDto.Create(characterEntity.Id, characterEntity.Type, characterEntity.Direction, new Vector3(characterEntity.Position.Value.X, characterEntity.Position.Value.Y), characterEntity.Speed);
        CreateCharacter(characterDto);
    }

    public void CreateCharacter(ICharacterDto characterDto)
    {
        Transform grid = transform;
        _currentGo = Instantiate(Resources.Load(characterDto.Image), characterDto.Position, Quaternion.identity) as GameObject;
        // instantiate and attach the component in once function
        CharacterMoveController controller = _container.InstantiateComponent<CharacterMoveController>(_currentGo);
        controller.SetId(characterDto.Id);
        _currentGo.tag = characterDto.Image;
        _currentGo.transform.parent = grid;
    }

}

