using System;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    [SerializeField] Vector3 min;
    [SerializeField] Vector3 max;

    public static Vector3 MIN;
    public static Vector3 MAX;

    private void Awake()
    {
        // must be loaded before TileController Start()
        MIN = min;
        MAX = max;
    }

    public List<GameObject> GetAllChilds()
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            list.Add(transform.GetChild(i).gameObject);
        }
        return list;
    }

    public GameObject FindTileById(Guid tileId)
    {
        List<GameObject> tiles = GetAllChilds();
        GameObject result = tiles[0];
        foreach (GameObject tile in tiles)
        {
            Guid id = tile.GetComponent<TileController>()._dto._id;
            if (id == tileId)
            {
                result = tile;
            }
        }
        return result;
    }
}
