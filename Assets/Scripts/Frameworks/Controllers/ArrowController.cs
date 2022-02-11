using UnityEngine;
using UnityEngine.EventSystems;
using Domain.Commons.Types;

public class ArrowController : MonoBehaviour
{
    public EnumDirection _direction;
    public LayerMask _puzzleLayer;

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
        }
    }
}
