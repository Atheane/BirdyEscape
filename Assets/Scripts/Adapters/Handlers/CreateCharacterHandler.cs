using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using UniMediator;
using Usecases.Characters.Commands;

public class CreateCharacterHandler : MonoBehaviour, ISingleMessageHandler<CreateCharacterCommand, Task>
{
    private DiContainer _container;
    private GameObject _currentGo;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
        Debug.Log("new CreateCharacterHandler");
    }

    public Task Handle(CreateCharacterCommand command)
    {
        Debug.Log("Handle()");
        Debug.Log("command");
        Debug.Log(command);
        return Task.CompletedTask;
    }

    //public void CreateCharacter(ICharacterDto characterDto)
    //{
    //    Transform grid = transform;
    //    _currentGo = Instantiate(Resources.Load(characterDto.Image), characterDto.Position, Quaternion.identity) as GameObject;
    //    // instantiate and attach the component in once function
    //    CharacterMoveController controller = _container.InstantiateComponent<CharacterMoveController>(_currentGo);
    //    controller.SetId(characterDto.Id);
    //    _currentGo.tag = characterDto.Image;
    //    _currentGo.transform.parent = grid;
    //}
}
