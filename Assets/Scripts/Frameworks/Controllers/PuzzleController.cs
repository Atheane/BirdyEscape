using UnityEngine;
using UnityEngine.EventSystems;
using Domain.Types;
using Zenject;

public class PuzzleController : MonoBehaviour
{
    [SerializeField] private LayerMask _layer;
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    private float minDistanceForSwipe = 20f;
    private bool detectSwipeOnlyAfterRelease = false;
    private DiContainer _container;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }
    public void Awake()
    {
        _layer = LayerMask.GetMask("Puzzle");
    }

    public void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _layer))
        {
            Debug.DrawLine(ray.origin, hit.point);
            if (hit.transform.tag == "Tile")
            {
                Debug.Log("maybe swipe");
                if (hit.transform.GetComponent<TileController>() != null)
                {
                    Debug.Log(hit.transform.GetComponent<TileController>()._dto._coordinates);
                }
            }
        }
    }
}
