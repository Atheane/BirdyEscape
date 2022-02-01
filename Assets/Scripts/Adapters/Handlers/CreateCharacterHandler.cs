using UnityEngine;
using Zenject;
using UniMediator;
using Libs.Domain.DomainEvents;
using Adapters.Unimediatr;

public class CreateCharacterHandler : MonoBehaviour, IMulticastMessageHandler<DomainEventNotification>
{
    private DiContainer _container;
    private GameObject _currentGo;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    public void Handle(DomainEventNotification notification)
    {
        Debug.Log("Handle");
        Debug.Log(notification);
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
