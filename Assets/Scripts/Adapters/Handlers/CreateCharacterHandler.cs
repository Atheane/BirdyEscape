using UnityEngine;
using UniMediator;
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
            characterEntity._position,
            characterEntity._speed);
    }
}
