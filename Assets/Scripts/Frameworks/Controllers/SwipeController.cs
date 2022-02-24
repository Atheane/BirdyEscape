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
                        DeleteArrow(target);
                        return;
                    }
                    if (!DetectTouch() && target.CompareTag(Entities.Tile.ToString()))
                    {
                        DrawArrow(target);
                        return;
                    }
                }
            }
        }  
    }

    private void DrawArrow(Transform tile)
    {
        EnumDirection direction = GetSwipeDirection();
        TileController tileController = tile.GetComponent<TileController>();
        if (tileController._dto._arrow == null)
        {
            // add arrow to tile
            var path = "Arrow/" + Entities.Arrow.ToString();
            _container.Resolve<CreateArrow>().Execute(
                new CreateArrowCommand(
                    tileController._dto._id,
                    direction,
                    _arrowPosition,
                    path
                )
            );
        } else {
            IArrowEntity arrowEntity = _container.Resolve<UpdateArrowDirection>().Execute(
                new UpdateArrowDirectionCommand(
                    tileController._dto._arrow._id,
                    direction
                )
            );
            IArrowDto arrowDto = ArrowDto.Create(
                arrowEntity._id,
                arrowEntity._direction,
                arrowEntity._coordinates,
                arrowEntity._path
            );
            Transform arrowTransform = tileController.GetArrow();
            arrowTransform.rotation = Quaternion.Euler(arrowDto._orientation);
        }
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
