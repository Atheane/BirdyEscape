using System;
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
    public CharacterDto _dto;

    public void Handle(DomainEventNotification<CharacterCreatedDomainEvent> notification)
    {
        Debug.Log("______" + notification._domainEvent._label + "_____handled");
        ICharacterEntity characterEntity = notification._domainEvent._props;
        _dto = CharacterDto.Create(
            characterEntity._id,
            characterEntity._type,
            characterEntity._direction,
            new Vector3(characterEntity._position.Value.X, Position3D.INIT_Y, characterEntity._position.Value.Z),
            characterEntity._speed);
    }
}
