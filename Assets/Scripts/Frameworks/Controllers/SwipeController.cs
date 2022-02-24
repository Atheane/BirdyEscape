using System;
using UnityEngine;
using Domain.Types;
using Zenject;
using Usecases;
using Usecases.Commands;
using Domain.Entities;
using Frameworks.Dtos;

public class SwipeController : MonoBehaviour
{
    [SerializeField] private LayerMask _layer;
    private Vector2 _fingerBeginPosition;
    private Vector2 _fingerEndPosition;
    private Vector3 _arrowPosition;

    private float _minDistanceForSwipe = 40f;

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
            Transform target = hit.transform;
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    _fingerBeginPosition = touch.position;
                    _fingerEndPosition = touch.position;
                    if (target.CompareTag(Entities.Tile.ToString())) {
                        try
                        {
                            _arrowPosition = target.parent.GetComponent<TileController>()._dto._position;
                        }
                        catch (Exception e)
                        {
                            Debug.Log(e);
                            _arrowPosition = target.GetComponent<TileController>()._dto._position;
                        }
                    }

                }
                if (touch.phase == TouchPhase.Ended)
                {
                    _fingerEndPosition = touch.position;
                    if (DetectTouch() && target.CompareTag(Entities.Arrow.ToString()))
                    {
                        Debug.Log(">>>>>>>>>>TOUCH ARROW");
                        DeleteArrow(target);
                    }
                    if (!DetectTouch() && target.CompareTag(Entities.Tile.ToString()))
                    {
                        Debug.Log(">>>>>>>>>>SWIPE ARROW");
                        CreateArrow();
                    }
                }
            }
        }  
    }

    private void CreateArrow()
    {
        EnumDirection direction = GetSwipeDirection();
        var path = "Arrow/" + Entities.Arrow.ToString();

        _container.Resolve<CreateArrow>().Execute(
            new CreateArrowCommand(
                direction,
                _arrowPosition,
                path
            )
        );
    }

    private void DeleteArrow(Transform target)
    {
        ArrowController controller = target.GetComponent<ArrowController>();
        _container.Resolve<DeleteArrow>().Execute(new DeleteArrowCommand(controller._dto._id));
        Destroy(target.gameObject);
    }

    private bool DetectTouch()
    {
        if (!SwipeDistanceCheckMet())
        {
            return true;
        }
        return false;
    }

    private EnumDirection GetSwipeDirection()
    {
        if (IsVerticalSwipe())
        {
            return _fingerEndPosition.y > _fingerBeginPosition.y ? EnumDirection.UP: EnumDirection.DOWN;
        }
        else
        {
            return _fingerEndPosition.x > _fingerBeginPosition.x ? EnumDirection.RIGHT : EnumDirection.LEFT;
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
