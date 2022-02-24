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
            Transform target = hit.transform;

            if (target.CompareTag(Entities.Arrow.ToString()))
            {
                foreach (Touch touch in Input.touches)
                {
                    ArrowController controller = target.GetComponent<ArrowController>();
                    if (touch.phase == TouchPhase.Began)
                    {
                        _arrowPosition = controller._dto._position;
                        _fingerBeginPosition = touch.position;
                        _fingerEndPosition = touch.position;
                    }
                    if (touch.phase == TouchPhase.Ended)
                    {
                        _fingerEndPosition = touch.position;
                        try
                        {
                            EnumDirection direction = DetectSwipe();
                            _container.Resolve<UpdateArrowDirection>().Execute(
                                new UpdateArrowDirectionCommand(
                                    controller._dto._id,
                                    direction
                                )
                            );
                        }
                        catch(Exception e)
                        {
                            Debug.Log(e);
                            _container.Resolve<DeleteArrow>().Execute(new DeleteArrowCommand(controller._dto._id));
                            Destroy(target.gameObject);
                        }
                    }
                }
            }
            else if (target.CompareTag(Entities.Tile.ToString()))
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        try
                        {
                            _arrowPosition = target.parent.GetComponent<TileController>()._dto._position;
                        }
                        catch (Exception e)
                        {
                            Debug.Log(e);
                            _arrowPosition = target.GetComponent<TileController>()._dto._position;
                        }
                        _fingerBeginPosition = touch.position;
                        _fingerEndPosition = touch.position;
                    }
                    if (touch.phase == TouchPhase.Ended)
                    {
                        _fingerEndPosition = touch.position;
                        try
                        {
                            EnumDirection direction = DetectSwipe();
                            var path = "Arrow/" + Entities.Arrow.ToString();

                            _container.Resolve<CreateArrow>().Execute(
                                new CreateArrowCommand(
                                    direction,
                                    _arrowPosition,
                                    path
                                )
                            );
                        } catch(Exception e)
                        {
                            Debug.LogException(e);
                        }

                    }
                }
            }
        }  
    }

    public EnumDirection DetectSwipe()
    {
        if (SwipeDistanceCheckMet())
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
        throw new Exception("NO_SWIPE_DETECTED");
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
