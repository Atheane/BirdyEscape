using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Domain.Commons.Types;
using Domain.Characters.Types;
using Domain.Characters.Constants;
using Usecases.Characters;
using Usecases.Characters.Commands;


public class GridController : MonoBehaviour
{
    private DiContainer _container;

    public Vector3 MainCharacterInitPosition;
    public EnumDirection MainCharacterInitDirection;
    //public Vector3 GridSize;
    //public GameObject LightTile;
    //public GameObject DarkTile;
    //public float Gutter;


    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    void Start()
    {
        //DrawGrid();
        _container.Resolve<CreateCharacter>().Execute(new CreateCharacterCommand(EnumCharacterType.BLACK_BIRD, MainCharacterInitDirection, (MainCharacterInitPosition[0], MainCharacterInitPosition[1], MainCharacterInitPosition[2]), Speed.INIT_SPEED));
    }

    //void DrawGrid()
    //{
    //    for (int z = 0; z < GridSize.z; z++)
    //    {
    //        for (int y = 0; y < GridSize.y; y++)
    //        {
    //            for (int x = 0; x < GridSize.x; x++)
    //            {
    //                if (x % 2 == 0 && z % 2 == 0 || x % 2 == 1 && z % 2 == 1)
    //                {
    //                    GameObject _currentGo = Instantiate(DarkTile, new Vector3(x * (1 + Gutter), y * (1 + Gutter), z * (1 + Gutter)), Quaternion.identity);
    //                    _currentGo.transform.parent = transform;
    //                }
    //                else
    //                {
    //                    GameObject _currentGo = Instantiate(LightTile, new Vector3(x * (1 + Gutter), y * (1 + Gutter), z * (1 + Gutter)), Quaternion.identity);
    //                    _currentGo.transform.parent = transform;
    //                }
    //            }
    //        }
    //    }
    //}

}

