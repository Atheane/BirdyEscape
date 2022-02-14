using UnityEngine;

using Zenject;
using Domain.Types;
using Domain.Constants;
using Usecases;
using Usecases.Commands;


public class GridController : MonoBehaviour
{
    //public Vector3 GridSize;
    //public GameObject LightTile;
    //public GameObject DarkTile;
    //public float Gutter;

    void Start()
    {
        //DrawGrid();
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

