using UnityEngine;
using Zenject;
using Usecases;
using Usecases.Commands;
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
        string path = "Resources/" + gameObject.name;
        float X = transform.position[0];
        float Y = transform.position[1];
        float Z = transform.position[2];
        _container.Resolve<CreateTile>().Execute(new CreateTileCommand((X, Y, Z), path));
    }
}
