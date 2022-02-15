using System;
using UnityEngine;
using UniMediator;
using Domain.DomainEvents;
using Domain.Entities;
using Adapters.Unimediatr;
using Frameworks.Dtos;

public class CreateTileHandler : MonoBehaviour, IMulticastMessageHandler<DomainEventNotification<TileCreatedDomainEvent>>
{
    public Guid _id;
    public Vector2 _coordinates;
    public string _path;
    private TileDto _dto;

    public void Handle(DomainEventNotification<TileCreatedDomainEvent> notification)
    {
        Debug.Log("______" + notification._domainEvent._label + "_____handled");
        ITileEntity tileEntity = notification._domainEvent._props;
        _dto = TileDto.Create(
            tileEntity._id,
            new Vector2(tileEntity._position.Value.X, tileEntity._position.Value.X),
            tileEntity._image.Value);
        SetData();
    }

    public void SetData()
    {
        _id = _dto._id;
        _coordinates = _dto._coordinates;
        _path = _dto._path;
    }
}

