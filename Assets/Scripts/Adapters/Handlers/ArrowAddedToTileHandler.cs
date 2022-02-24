using System;
using UnityEngine;
using Zenject;
using UniMediator;
using Domain.DomainEvents;
using Domain.Entities;
using Adapters.Unimediatr;
using Frameworks.Dtos;

public class ArrowAddedToTileHandler : MonoBehaviour, IMulticastMessageHandler<DomainEventNotification<ArrowAddedToTileDomainEvent>>
{

    private DiContainer _container;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    public TileDto _dto;

    public void Handle(DomainEventNotification<ArrowAddedToTileDomainEvent> notification)
    {
        Debug.Log("______" + notification._domainEvent._label + "_____handled");
        ITileEntity tileEntity = notification._domainEvent._props;
        _dto = TileDto.Create(
            tileEntity._id,
            tileEntity._coordinates,
            tileEntity._path
        );
        IArrowDto arrowDto = ArrowDto.Create(
            tileEntity._arrow._id,
            tileEntity._arrow._direction,
            tileEntity._arrow._coordinates,
            tileEntity._arrow._path
        );
        _dto.AddArrow(arrowDto);
    }

    public void DrawArrow(IArrowDto dto)
    {
        Transform tile = transform;
        GameObject go = Instantiate(Resources.Load(dto._path), dto._position, Quaternion.Euler(dto._orientation)) as GameObject;
        // instantiate and attach the component in once function
        var controller = _container.InstantiateComponent<ArrowController>(go);
        controller._dto = dto;
        go.tag = Entities.Arrow.ToString();
        go.transform.parent = tile;
    }
}
