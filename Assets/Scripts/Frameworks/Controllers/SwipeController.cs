using System;
using UnityEngine;
using UniMediator;
using Zenject;
using Adapters.Unimediatr;
using Usecases;
using Usecases.Commands;
using Domain.Types;
using Domain.DomainEvents;
using Domain.Entities;
using Frameworks.Dtos;

public class SwipeController :
    MonoBehaviour,
    IMulticastMessageHandler<DomainEventNotification<LevelMovePlayed>>,
    IMulticastMessageHandler<DomainEventNotification<LevelRestarted>>
{
    [SerializeField] private LayerMask _layer;
    private Vector2 _fingerBeginPosition;
    private Vector2 _fingerEndPosition;
    private Transform _target;
    private float _minDistanceForSwipe = 40f;

    private DiContainer _container;
    private EnumLevelState _levelState;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }
    public void Awake()
    {
        _layer = LayerMask.GetMask(Entities.Puzzle.ToString());
        _levelState = EnumLevelState.ON;
    }

    public void Handle(DomainEventNotification<LevelMovePlayed> notification)
    {
        Debug.Log("______" + notification._domainEvent._label + "_____handled");
        ILevelEntity levelEntity = notification._domainEvent._props;
        _levelState = levelEntity._state;
    }

    public void Handle(DomainEventNotification<LevelRestarted> notification)
    {
        Debug.Log("______" + notification._domainEvent._label + "_____handled");
        ILevelEntity levelEntity = notification._domainEvent._props;
        _levelState = levelEntity._state;
    }

    public void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _layer))
        {
            if (_levelState == EnumLevelState.ON)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        _target = hit.transform;
                        _fingerBeginPosition = touch.position;
                        _fingerEndPosition = touch.position;
                    }
                    if (_target != null && touch.phase == TouchPhase.Ended)
                    {
                        _fingerEndPosition = touch.position;
                        if (!DetectTouch() && _target.CompareTag(Entities.Tile.ToString()))
                        {
                            DrawArrow();
                            return;
                        }
                        if (!DetectTouch() && _target.CompareTag(Entities.Arrow.ToString()))
                        {
                            ChangeArrowDirection();
                            return;
                        }
                        if (DetectTouch() && _target.CompareTag(Entities.Arrow.ToString()))
                        {
                            DeleteArrow();
                            return;
                        }
                    }
                }
            }
        }  
    }

    private void DrawArrow()
    {
        EnumDirection direction = GetSwipeDirection();
        var path = "Arrow/" + Entities.Arrow.ToString();
        _container.Resolve<AddTileArrow>().Execute(
            new AddTileArrowCommand(
                _target.GetComponent<TileController>()._dto._id,
                direction,
                path,
                false
            )
        );
    }

    private void ChangeArrowDirection()
    {
        EnumDirection direction = GetSwipeDirection();
        TileController tile = _target.parent.GetComponent<TileController>();
        _container.Resolve<UpdateTileArrowDirection>().Execute(
                new UpdateTileArrowDirectionCommand(
                    tile._dto._id,
                    direction
                )
            );
    }

    private void DeleteArrow()
    {
        TileController controller = _target.parent.GetComponent<TileController>();
        Destroy(_target.gameObject);
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
