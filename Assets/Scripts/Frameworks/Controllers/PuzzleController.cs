using System;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    //[SerializeField] Vector3 min;
    //[SerializeField] Vector3 max;

    public static Vector3 MIN = new Vector3(-12.5f, -9.5f, -5f);
    public static Vector3 MAX = new Vector3(-5.5f, 0f, 6f);

    public List<GameObject> tiles;

    private void Start()
    {
        //MIN = min;
        //MAX = max;
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

            if (child.TryGetComponent(out TileController tileController))
            {

                tiles.Add(child.gameObject);
            }
            // reccursive search for child
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
