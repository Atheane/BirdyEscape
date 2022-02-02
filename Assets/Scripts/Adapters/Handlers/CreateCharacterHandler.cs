using UnityEngine;
using Zenject;
using UniMediator;
using Domain.Characters.DomainEvents;
using Domain.Characters.Entities;
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
        ICharacterEntity characterEntity = notification._domainEvent._props;
        var characterDto = CharacterDto.Create(characterEntity.Id, characterEntity.Type, characterEntity.Direction, new Vector3(characterEntity.Position.Value.X, characterEntity.Position.Value.Y), characterEntity.Speed);
        CreateCharacter(characterDto);
    }

    public void CreateCharacter(ICharacterDto characterDto)
    {
        Transform grid = transform.parent;
        GameObject _currentGo = Instantiate(Resources.Load(characterDto.Image), characterDto.Position, Quaternion.identity) as GameObject;
        // instantiate and attach the component in once function
        CharacterMoveController controller = _container.InstantiateComponent<CharacterMoveController>(_currentGo);
        controller.SetId(characterDto.Id);
        _currentGo.tag = characterDto.Image;
        _currentGo.transform.parent = grid;
    }
}
