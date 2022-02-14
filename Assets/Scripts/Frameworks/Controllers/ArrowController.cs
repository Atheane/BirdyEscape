using UnityEngine;
using UnityEngine.EventSystems;
using Domain.Types;
using Zenject;

public class ArrowController : MonoBehaviour
{
    public EnumDirection _direction;
    public LayerMask _puzzleLayer;
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

    public void Start()
    {
        _puzzleLayer = LayerMask.GetMask("Puzzle");
    }

    public void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _puzzleLayer))
        {
            Debug.DrawLine(ray.origin, hit.point);
            Debug.Log("Cube clicked");
            Debug.Log(hit.point);
        }
    }
}
