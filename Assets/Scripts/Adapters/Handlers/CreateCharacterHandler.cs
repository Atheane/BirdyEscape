using UnityEngine;
using Zenject;
using UniMediator;
using Domain.Constants;
using Domain.DomainEvents;
using Domain.Entities;
using Adapters.Unimediatr;
using Frameworks.Dtos;

public class CreateCharacterHandler : MonoBehaviour, IMulticastMessageHandler<DomainEventNotification<CharacterCreatedDomainEvent>>
{
    private DiContainer _container;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    public void Handle(DomainEventNotification<CharacterCreatedDomainEvent> notification)
    {
        Debug.Log("______" + notification._domainEvent._label + "_____handled");
        ICharacterEntity characterEntity = notification._domainEvent._props;
        var characterDto = CharacterDto.Create(
            characterEntity._id,
            characterEntity._type,
            characterEntity._direction,
            new Vector3(characterEntity._position.Value.X, Position3D.INIT_Y, characterEntity._position.Value.Z),
            characterEntity._speed);
        CreateCharacter(characterDto);
    }

    public void CreateCharacter(ICharacterDto characterDto)
    {
        Transform grid = transform.parent;
        GameObject _currentGo = Instantiate(Resources.Load(characterDto._image), characterDto._position, Quaternion.Euler(characterDto._orientation)) as GameObject;
        // instantiate and attach the component in once function
        CharacterMoveController controller = _container.InstantiateComponent<CharacterMoveController>(_currentGo);
        //controller.SetId(characterDto._id);
        _currentGo.tag = characterDto._image;
        _currentGo.transform.parent = grid;
    }
}
