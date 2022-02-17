using UnityEngine;
using Zenject;
using Usecases;
using Usecases.Commands;
using Domain.Entities;
using Frameworks.Dtos;


public class TileController : MonoBehaviour
{
    private DiContainer _container;
    public TileDto _dto;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
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
    }
}
