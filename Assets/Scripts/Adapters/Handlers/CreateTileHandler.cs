using UnityEngine;
using UniMediator;
using Domain.DomainEvents;
using Domain.Entities;
using Adapters.Unimediatr;
using Frameworks.Dtos;

public class CreateTileHandler : MonoBehaviour, ISingleMessageHandler<DomainEventNotification<TileCreatedDomainEvent>, string>
{
    public TileDto _dto;

    public string Handle(DomainEventNotification<TileCreatedDomainEvent> notification)
    {
        string log = "______" + notification._domainEvent._label + "_____handled";
        Debug.Log(log);
        ITileEntity tileEntity = notification._domainEvent._props;
        _dto = TileDto.Create(
            tileEntity._id,
            tileEntity._coordinates,
            tileEntity._path);
        return log;
    }
}

