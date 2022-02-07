using UnityEngine;
using UniMediator;
using Domain.Characters.Constants;
using Domain.Characters.DomainEvents;
using Domain.Characters.Entities;
using Adapters.Unimediatr;
using Frameworks.Dtos;

public class UpdateCharacterDirectionHandler : MonoBehaviour, IMulticastMessageHandler<DomainEventNotification<CharacterDirUpdatedDomainEvent>>
{

    public void Handle(DomainEventNotification<CharacterDirUpdatedDomainEvent> notification)
    {
        Debug.Log("______" + notification._domainEvent._label + "_____handled");
        ICharacterEntity characterEntity = notification._domainEvent._props;
        var characterDto = CharacterDto.Create(characterEntity.Id, characterEntity.Type, characterEntity.Direction, new Vector3(characterEntity.Position.Value.X, Position.INIT_Y, characterEntity.Position.Value.Z), characterEntity.Speed);
        RotateCharacter(characterDto);
    }

    public void RotateCharacter(ICharacterDto characterDto)
    {
        GameObject bird = GameObject.FindGameObjectWithTag(characterDto.Image);
        bird.transform.rotation = Quaternion.Euler(characterDto.Orientation);
    }
}
