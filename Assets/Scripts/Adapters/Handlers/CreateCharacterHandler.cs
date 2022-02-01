using UnityEngine;
using Zenject;
using UniMediator;
using Domain.Characters.DomainEvents;
using Usecases.Characters.Commands;
using Adapters.Unimediatr;
using Frameworks.Dtos;

public class CreateCharacterHandler : MonoBehaviour, IMulticastMessageHandler<DomainEventNotification<CharacterCreatedDomainEvent>>
{
    private DiContainer _container;
    private GameObject _currentGo;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    public void Handle(DomainEventNotification<CharacterCreatedDomainEvent> notification)
    {
        Debug.Log("Handle");
        Debug.Log(notification);
        Debug.Log(notification._domainEvent.Props);
        //CharacterDto.Create(characterEntity.Id, characterEntity.Type, characterEntity.Direction, new Vector3(characterEntity.Position.Value.X, characterEntity.Position.Value.Y), characterEntity.Speed);
        //ICharacterDto characterDto = CharacterDto.Create(domainEvent.Id, domainEvent.)
    }

    public void CreateCharacter(ICharacterDto characterDto)
    {
        Transform grid = transform.parent;
        _currentGo = Instantiate(Resources.Load(characterDto.Image), characterDto.Position, Quaternion.identity) as GameObject;
        // instantiate and attach the component in once function
        CharacterMoveController controller = _container.InstantiateComponent<CharacterMoveController>(_currentGo);
        controller.SetId(characterDto.Id);
        _currentGo.tag = characterDto.Image;
        _currentGo.transform.parent = grid;
    }
}
