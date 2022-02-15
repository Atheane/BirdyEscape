using UnityEngine;
using UniMediator;
using Domain.DomainEvents;
using Domain.Entities;
using Domain.Constants;
using Adapters.Unimediatr;
using Frameworks.Dtos;

public class UpdateCharacterDirectionHandler : MonoBehaviour, IMulticastMessageHandler<DomainEventNotification<CharacterDirUpdatedDomainEvent>>
{

    public void Handle(DomainEventNotification<CharacterDirUpdatedDomainEvent> notification)
    {
        Debug.Log("______" + notification._domainEvent._label + "_____handled");
        ICharacterEntity characterEntity = notification._domainEvent._props;
        var characterDto = CharacterDto.Create(
            characterEntity._id,
            characterEntity._type,
            characterEntity._direction,
            new Vector3(characterEntity._position.Value.X, Position3D.INIT_Y, characterEntity._position.Value.Z),
            characterEntity._speed);
        RotateCharacter(characterDto);
    }

    public void RotateCharacter(ICharacterDto characterDto)
    {
        GameObject bird = GameObject.FindGameObjectWithTag(characterDto._image);
        bird.transform.rotation = Quaternion.Euler(characterDto._orientation);
    }
}
