using UnityEngine;
using Zenject;
using Usecases;
using Usecases.Commands;
using Domain.Entities;
using Frameworks.Dtos;
using System.Collections.Generic;


public class TileController : MonoBehaviour
{
    private DiContainer _container;
    public TileDto _dto;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    public static List<GameObject> GetAllChilds(GameObject Go)
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < Go.transform.childCount; i++)
        {
            list.Add(Go.transform.GetChild(i).gameObject);
        }
        return list;
    }

    void Awake()
    {
        string path = gameObject.name;
        ITileEntity tileEntity = _container.Resolve<CreateTile>().Execute(new CreateTileCommand(transform.position, path));
        _dto = TileDto.Create(
            tileEntity._id,
            tileEntity._coordinates,
            tileEntity._path
        );
        List<GameObject> children = GetAllChilds(gameObject);
        foreach(GameObject child in children)
        {
            child.tag = gameObject.tag;
        }
    }

    public Transform GetArrow()
    {
        return transform.Find(Entities.Arrow.ToString());
    }

}
