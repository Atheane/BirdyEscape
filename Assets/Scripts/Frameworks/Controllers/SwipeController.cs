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
    private Vector3 _fingerBeginPosition;
    private Vector3 _fingerEndPosition;

    private Vector2 _arrowCoordinates;
    private float _minDistanceForSwipe = 20f;

    private DiContainer _container;
    public IArrowDto _arrowDto;

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
            //Debug.DrawLine(ray.origin, hit.point);
            if (hit.transform.tag == Entities.Tile.ToString())
            {
                _arrowCoordinates = hit.transform.gameObject.GetComponent<TileController>()._dto._coordinates;
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
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
            IArrowEntity arrowEntity = _container.Resolve<CreateArrow>().Execute(
                new CreateArrowCommand(
                    direction,
                    ((int)_arrowCoordinates.x, (int)_arrowCoordinates.y),
                    Entities.Arrow.ToString()
                )
            );
            Debug.Log(arrowEntity);
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
