using UnityEngine;
using UniMediator;
using Domain.DomainEvents;
using Domain.Entities;
using Adapters.Unimediatr;
using Frameworks.Dtos;
using System;

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
            characterEntity._position,
            characterEntity._speed);
        RotateCharacter(characterDto);
    }

    public void RotateCharacter(ICharacterDto characterDto)
    {
        try
        {
            GameObject bird = GameObject.FindGameObjectWithTag(characterDto._image);
            bird.transform.rotation = Quaternion.Euler(characterDto._orientation);
        } catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
}
