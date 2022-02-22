using System;
using UnityEngine;
using Domain.Types;
using Zenject;
using Usecases;
using Usecases.Commands;

public class SwipeController : MonoBehaviour
{
    [SerializeField] private LayerMask _layer;
    private Vector3 _fingerBeginPosition;
    private Vector3 _fingerEndPosition;

    private Vector3 _arrowPosition;
    private float _minDistanceForSwipe = 20f;

    private DiContainer _container;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }
    public void Awake()
    {
        _layer = LayerMask.GetMask(Entities.Puzzle.ToString());
    }

    public void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _layer))
        {
            Debug.DrawRay(ray.origin, hit.point);
            Debug.Log(hit.transform.tag);
            if (hit.transform.CompareTag(Entities.Tile.ToString()))
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        Vector3 tilePosition;
                        try
                        {
                            tilePosition = hit.transform.GetComponent<TileController>()._dto._position;
                        }
                        catch (Exception e)
                        {
                            tilePosition = hit.transform.parent.GetComponent<TileController>()._dto._position;
                        }
                        _arrowPosition = tilePosition;
                        _fingerBeginPosition = touch.position;
                        _fingerEndPosition = touch.position;
                    }
                    if (touch.phase == TouchPhase.Ended)
                    {
                        _fingerEndPosition = touch.position;
                        DetectSwipe();
                    }
                }
            }
            
        }
    }

    public void DetectSwipe()
    {
        if (SwipeDistanceCheckMet())
        {
            EnumDirection direction;
            if (IsVerticalSwipe())
            {
                direction = _fingerEndPosition.y > _fingerBeginPosition.y ? EnumDirection.UP: EnumDirection.DOWN;
            }
            else
            {
                direction = _fingerEndPosition.x > _fingerBeginPosition.x ? EnumDirection.RIGHT : EnumDirection.LEFT;
                
            }
            var path = "Arrow/" + Entities.Arrow.ToString();

            _container.Resolve<CreateArrow>().Execute(
                new CreateArrowCommand(
                    direction,
                    _arrowPosition,
                    path
                )
            );
        }
    }

    private bool IsVerticalSwipe()
    {
        return VerticalMovementDistance() > HorizontalMovementDistance();
    }

    private bool SwipeDistanceCheckMet()
    {
        return VerticalMovementDistance() > _minDistanceForSwipe || HorizontalMovementDistance() > _minDistanceForSwipe;
    }

    private float VerticalMovementDistance()
    {
        return Mathf.Abs(_fingerEndPosition.y - _fingerBeginPosition.y);
    }

    private float HorizontalMovementDistance()
    {
        return Mathf.Abs(_fingerEndPosition.x - _fingerBeginPosition.x);
    }

}
