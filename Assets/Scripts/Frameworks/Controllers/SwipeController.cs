using System;
using UnityEngine;
using Domain.Types;
using Zenject;
using Usecases;
using Usecases.Commands;

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
            if (hit.transform.CompareTag(Entities.Arrow.ToString()))
            {
                foreach (Touch touch in Input.touches)
                {
                    ArrowController controller = hit.transform.GetComponent<ArrowController>();
                    if (touch.phase == TouchPhase.Began)
                    {
                        _arrowPosition = controller._dto._position;
                        _fingerBeginPosition = touch.position;
                        _fingerEndPosition = touch.position;
                    }
                    if (touch.phase == TouchPhase.Ended)
                    {
                        _fingerEndPosition = touch.position;
                        DetectSwipe();
                        _container.Resolve<UpdateArrowDirection>().Execute(
                            new UpdateArrowDirectionCommand(
                                controller._dto._id,
                                controller._dto._direction
                            )
                        );
                    }
                }
            }
            else if (hit.transform.CompareTag(Entities.Tile.ToString()))
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        try
                        {
                            _arrowPosition = hit.transform.parent.GetComponent<TileController>()._dto._position;
                        }
                        catch (Exception e)
                        {
                            Debug.Log(e);
                            _arrowPosition = hit.transform.GetComponent<TileController>()._dto._position;
                        }
                        _fingerBeginPosition = touch.position;
                        _fingerEndPosition = touch.position;
                    }
                    if (touch.phase == TouchPhase.Ended)
                    {
                        _fingerEndPosition = touch.position;
                        EnumDirection direction = DetectSwipe();
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
            }
        }  
    }

    public EnumDirection DetectSwipe()
    {
        EnumDirection direction = EnumDirection.EMPTY;
        if (SwipeDistanceCheckMet())
        {
            if (IsVerticalSwipe())
            {
                direction = _fingerEndPosition.y > _fingerBeginPosition.y ? EnumDirection.UP: EnumDirection.DOWN;
            }
            else
            {
                direction = _fingerEndPosition.x > _fingerBeginPosition.x ? EnumDirection.RIGHT : EnumDirection.LEFT;
            }
        }
        return direction;
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
