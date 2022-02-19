using UnityEngine;
using UniMediator;
using Domain.Constants;
using Domain.DomainEvents;
using Domain.Entities;
using Adapters.Unimediatr;
using Frameworks.Dtos;

public class CreateCharacterHandler : MonoBehaviour, ISingleMessageHandler<DomainEventNotification<CharacterCreatedDomainEvent>, string>
{
    public CharacterDto _dto;

    public string Handle(DomainEventNotification<CharacterCreatedDomainEvent> notification)
    {
        string log = "______" + notification._domainEvent._label + "_____handled";
        Debug.Log(log); ICharacterEntity characterEntity = notification._domainEvent._props;
        _dto = CharacterDto.Create(
            characterEntity._id,
            characterEntity._type,
            characterEntity._direction,
            characterEntity._position,
            characterEntity._speed);
        return log;
    }
}
