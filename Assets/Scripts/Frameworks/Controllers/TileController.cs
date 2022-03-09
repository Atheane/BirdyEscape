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
    public TileDto _dto;

    public void Handle(DomainEventNotification<TileArrowRemoved> notification)
    {
        Debug.Log("______" + notification._domainEvent._label + "_____handled");
        ITileEntity tileEntity = notification._domainEvent._props;
        _dto = TileDto.Create(
            tileEntity._id,
            tileEntity._coordinates,
            tileEntity._path);
        List<GameObject> children = GetAllChildren(gameObject);
        foreach (GameObject child in children)
        {
            if (child.CompareTag(Entities.Arrow.ToString()))
            {
                Destroy(child);
            }
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
