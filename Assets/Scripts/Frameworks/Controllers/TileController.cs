
using System.Collections.Generic;
using UnityEngine;
using UniMediator;
using Domain.DomainEvents;
using Domain.Entities;
using Domain.Types;
using Adapters.Unimediatr;
using Frameworks.Dtos;

public class TileController : MonoBehaviour, IMulticastMessageHandler<DomainEventNotification<TileArrowRemoved>>
{
    public TileDto _dto { get; private set; }

    public void SetDto(TileDto dto)
    {
        _dto = dto;
    }

    public void Handle(DomainEventNotification<TileArrowRemoved> notification)
    {
        Debug.Log("______" + notification._domainEvent._label + "_____handled");
        ITileEntity tile = notification._domainEvent._props;
        if (tile._id == _dto._id)
        {
            TileDto updatedDto = TileDto.Create(
                tile._id,
                tile._coordinates);
            SetDto(updatedDto);
            List<GameObject> children = GetAllChildren(gameObject);
            foreach (GameObject child in children)
            {
                if (child.CompareTag(Entities.Arrow.ToString()))
                {
                    Destroy(child);
                }
            }
        }
    }

    public void Handle(DomainEventNotification<TileArrowAdded> notification)
    {
        Debug.Log("______" + notification._domainEvent._label + "_____handled");
        ITileEntity tile = notification._domainEvent._props;
        if (_dto._id == tile._id && _dto._arrow == null)
        {
            IArrowDto dto = ArrowDto.Create(
                tile._arrow._id,
                tile._arrow._direction,
                tile._arrow._coordinates,
                tile._arrow._path,
                tile._arrow._effectOnce
            );
            _dto.AddArrow(dto);
            GameObject go = Instantiate(Resources.Load(dto._path), dto._position, Quaternion.Euler(dto._orientation)) as GameObject;
            // instantiate and attach the component in one function
            // todo detect of arrow is near a wall with raycast
            // if so: update its effectDuration
            go.tag = Entities.Arrow.ToString();
            go.transform.parent = transform;
        }
    }

    private static List<GameObject> GetAllChildren(GameObject Go)
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < Go.transform.childCount; i++)
        {
            list.Add(Go.transform.GetChild(i).gameObject);
        }
        return list;
    }

    private void Start()
    {
        List<GameObject> children = GetAllChildren(gameObject);
        foreach(GameObject child in children)
        {
            child.tag = gameObject.tag;
        }
    }

}
