using UnityEngine;
using UniMediator;
using Domain.DomainEvents;
using Domain.Entities;
using Adapters.Unimediatr;
using Frameworks.Dtos;
using System;

public class UpdateCharacterDirectionHandler : MonoBehaviour, ISingleMessageHandler<DomainEventNotification<CharacterDirUpdatedDomainEvent>, string>
{

    public string Handle(DomainEventNotification<CharacterDirUpdatedDomainEvent> notification)
    {
        string log = "______" + notification._domainEvent._label + "_____handled";
        Debug.Log(log);
        ICharacterEntity characterEntity = notification._domainEvent._props;
        var characterDto = CharacterDto.Create(
            characterEntity._id,
            characterEntity._type,
            characterEntity._direction,
            characterEntity._position,
            characterEntity._speed);
        RotateCharacter(characterDto);
        return log;
    }

    public void RotateCharacter(ICharacterDto characterDto)
    {
        try
        {
            gameObject.transform.rotation = Quaternion.Euler(characterDto._orientation);
        } catch(Exception e)
        {
            Debug.LogException(e);
        }

    }
}
