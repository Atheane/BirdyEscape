using UnityEngine;
using Zenject;
using UniMediator;
using Domain.DomainEvents;
using Domain.Entities;
using Adapters.Unimediatr;
using Frameworks.Dtos;
using System;

public class ArrowDeletedHandler : MonoBehaviour, IMulticastMessageHandler<DomainEventNotification<ArrowDeletedDomainEvent>>
{
    public void Handle(DomainEventNotification<ArrowDeletedDomainEvent> notification)
    {
        Debug.Log("______" + notification._domainEvent._label + "_____handled");
        IArrowEntity arrowEntity = notification._domainEvent._props;
        IArrowDto dto = ArrowDto.Create(
            arrowEntity._id,
            arrowEntity._direction,
            arrowEntity._coordinates,
            arrowEntity._path
        );
        DeleteArrow(dto);
    }

    public void DeleteArrow(IArrowDto dto)
    {
        Component[] arrowControllers = GetComponents<ArrowController>();
        foreach(ArrowController arrow in arrowControllers)
        {
            if (arrow._id == dto._id)
            {
                Destroy(arrow.gameObject);
            }
        }
    }
}
