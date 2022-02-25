using System;
using UnityEngine;
using Zenject;
using UniMediator;
using Domain.DomainEvents;
using Domain.Entities;
using Adapters.Unimediatr;
using Frameworks.Dtos;

public class TileArrowAddedHandler : MonoBehaviour, IMulticastMessageHandler<DomainEventNotification<TileArrowAdded>>
{
    private DiContainer _container;


    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    public void Handle(DomainEventNotification<TileArrowAdded> notification)
    {
        Debug.Log("______" + notification._domainEvent._label + "_____handled");
        ITileEntity tile = notification._domainEvent._props;
        IArrowDto dto = ArrowDto.Create(
            tile._arrow._id,
            tile._arrow._direction,
            tile._arrow._coordinates,
            tile._arrow._path
        );
        AddArrowToTile(tile._id, dto);
    }

    public void AddArrowToTile(Guid tileId, IArrowDto dto)
    {
        GameObject tile = transform.GetComponent<PuzzleController>().FindTileById(tileId);
        ITileDto tileDto = tile.GetComponent<TileController>()._dto;
        tileDto.AddArrow(dto);
        GameObject go = Instantiate(Resources.Load(dto._path), dto._position, Quaternion.Euler(dto._orientation)) as GameObject;
        // instantiate and attach the component in once function
        go.tag = Entities.Arrow.ToString();
        go.transform.parent = tile.transform;
    }
}
