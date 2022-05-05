
using System.Collections.Generic;
using UnityEngine;
using UniMediator;
using Zenject;
using Domain.DomainEvents;
using Domain.Entities;
using Domain.Types;
using Usecases;
using Usecases.Commands;
using Adapters.Unimediatr;
using Frameworks.Dtos;

public class TileController : MonoBehaviour,
    IMulticastMessageHandler<DomainEventNotification<TileArrowAdded>>,
    IMulticastMessageHandler<DomainEventNotification<TileArrowRemoved>>,
    IMulticastMessageHandler<DomainEventNotification<TileArrowEffectUpdated>>
{
    public TileDto _dto { get; private set; }

    public void SetDto(TileDto dto)
    {
        _dto = dto;
    }

    private LayerMask _layerObstacle;
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
        if (_dto._id == tile._id && _dto._arrow == null)
        {
            IArrowDto dto = ArrowDto.Create(
                tile._arrow._id,
                tile._arrow._direction,
                tile._arrow._coordinates,
                tile._arrow._path,
                tile._arrow._effectOnce,
                tile._arrow._numberEffects
            );
            _dto.AddOrUpdateArrow(dto);
            // instantiate and attach the component in one function
            GameObject go = Instantiate(Resources.Load(dto._path), dto._position, Quaternion.Euler(dto._orientation)) as GameObject;
            // if arrow points to an obstacle, should update its effectDuration to effectOnce
            if (CollisionWithObstacle(go))
            {
                _container.Resolve<UpdateTileArrowEffect>().Execute(new UpdateTileArrowEffectCommand(_dto._id, 0));
            }
            go.tag = Entities.Arrow.ToString();
            go.transform.parent = transform;
        }
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

    public void Handle(DomainEventNotification<TileArrowEffectUpdated> notification)
    {
        Debug.Log("______" + notification._domainEvent._label + "_____handled");
        ITileEntity tile = notification._domainEvent._props;
        if (_dto._id == tile._id && _dto._arrow != null)
        {
            IArrowDto dto = ArrowDto.Create(
                tile._arrow._id,
                tile._arrow._direction,
                tile._arrow._coordinates,
                tile._arrow._path,
                tile._arrow._effectOnce,
                tile._arrow._numberEffects
            );
            _dto.AddOrUpdateArrow(dto);
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
        _layerObstacle = LayerMask.GetMask("Obstacle");
    }

    // detect if arrow is near a wall with raycast
    private bool CollisionWithObstacle(GameObject go)
    {
        Ray ray = new Ray(go.transform.position, -go.transform.right);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction);
        if (Physics.Raycast(ray, out hit, 0.6f, _layerObstacle))
        {
            return true;
        }
        return false;
    }

}
