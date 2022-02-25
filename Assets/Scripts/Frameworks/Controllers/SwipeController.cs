using System;
using UnityEngine;
using Domain.Types;
using Zenject;
using Usecases;
using Usecases.Commands;
using Domain.Entities;

public class SwipeController : MonoBehaviour
{
    [SerializeField] private LayerMask _layer;
    private Vector2 _fingerBeginPosition;
    private Vector2 _fingerEndPosition;
    private Transform _target;
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
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    _target = hit.transform;
                    _fingerBeginPosition = touch.position;
                    _fingerEndPosition = touch.position;
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    _fingerEndPosition = touch.position;
                    if (DetectTouch())
                    {
                        DeleteArrow(_target);
                        return;
                    }
                    if (!DetectTouch())
                    {
                        DrawArrow(_target);
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
            Debug.Log("________________");
            Debug.Log(tileController.tag);
            Debug.Log(tileController._dto._id);
            var path = "Arrow/" + Entities.Arrow.ToString();
            _container.Resolve<AddTileArrow>().Execute(
                new AddTileArrowCommand(
                    tileController._dto._id,
                    direction,
                    path
                )
            );
        } else {
            ITileEntity tileEntity = _container.Resolve<UpdateTileArrowDirection>().Execute(
                new UpdateTileArrowDirectionCommand(
                    tileController._dto._id,
                    direction
                )
            );
            Debug.Log(tileEntity);

            //Transform arrowTransform = tileController.GetArrow();
            //arrowTransform.rotation = Quaternion.Euler(arrowDto._orientation);
        }
    }

    private void DeleteArrow(Transform target)
    {
        TileController controller = target.parent.GetComponent<TileController>();
        Destroy(target.gameObject);
        _container.Resolve<RemoveTileArrow>().Execute(new RemoveTileArrowCommand(controller._dto._id));
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
