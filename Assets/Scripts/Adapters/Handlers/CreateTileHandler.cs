using System;
using UnityEngine;
using UniMediator;
using Domain.DomainEvents;
using Domain.Entities;
using Adapters.Unimediatr;
using Frameworks.Dtos;

public class CreateTileHandler : MonoBehaviour, IMulticastMessageHandler<DomainEventNotification<TileCreatedDomainEvent>>
{
    public TileDto _dto;

    public void Handle(DomainEventNotification<TileCreatedDomainEvent> notification)
    {
        Debug.Log("______" + notification._domainEvent._label + "_____handled");
        ITileEntity tileEntity = notification._domainEvent._props;
        _dto = TileDto.Create(
            tileEntity._id,
            tileEntity._coordinates,
            tileEntity._path);
    }
}

