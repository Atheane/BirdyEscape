using System;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    [SerializeField] Vector3 min;
    [SerializeField] Vector3 max;

    public static Vector3 MIN;
    public static Vector3 MAX;

    public List<GameObject> tiles;

    private void Awake()
    {
        // must be loaded before TileController Start()
        MIN = min;
        MAX = max;
    }

    private void Start()
    {
        SetTiles(gameObject);
    }

    private void SetTiles(GameObject obj)
    {
        if (null == obj)
            return;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
                continue;
            //child.gameobject contains the current child you can do whatever you want like add it to an array
            if (child.TryGetComponent(out TileController tileController))
            {
                tiles.Add(child.gameObject);
            }
            SetTiles(child.gameObject);
        }
    }

    public GameObject FindTileById(Guid tileId)
    {
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
