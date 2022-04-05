using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using UniMediator;
using Usecases;
using Usecases.Commands;
using Adapters.Unimediatr;
using Domain.DomainEvents;
using Domain.ValueObjects;
using Domain.Types;
using Domain.Entities;
using Frameworks.Dtos;

public class CharacterMoveController :
    MonoBehaviour,
    IMulticastMessageHandler<DomainEventNotification<LevelRestarted>>,
    IMulticastMessageHandler<DomainEventNotification<CharacterStateUpdated>>
{
    public EnumCharacterType _type;
    public Vector3 _init_position;
    public EnumDirection _init_direction;
    public float _speed;

    public CharacterDto _dto { get; private set; }

    private LayerMask _layerObstacle;
    private LayerMask _layerArrow;
    private LayerMask _layerExit;

    private DiContainer _container;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    public void Handle(DomainEventNotification<LevelRestarted> notification)
    {
        Debug.Log("______" + notification._domainEvent._label + "_____handled");
        ILevelEntity levelEntity = notification._domainEvent._props;
        // characters
        ICharacterEntity[] charactersEntity = levelEntity._characters;
        foreach (ICharacterEntity characterEntity in charactersEntity)
        {
            if (characterEntity._id == _dto._id)
            {
                CharacterDto dto = CharacterDto.Create(
                    characterEntity._id,
                    characterEntity._type,
                    characterEntity._direction,
                    characterEntity._state,
                    characterEntity._position,
                    characterEntity._speed,
                    characterEntity._totalDistance
                );
                SetDto(dto);
                transform.position = _dto._position;
                transform.rotation = Quaternion.Euler(_dto._orientation);
            }
        }
    }

    public void Handle(DomainEventNotification<CharacterStateUpdated> notification)
    {
        Debug.Log("______" + notification._domainEvent._label + "_____handled");
        ICharacterEntity characterEntity = notification._domainEvent._props;
        // characters
        SetDto(CharacterDto.Create(
            characterEntity._id,
            characterEntity._type,
            characterEntity._direction,
            characterEntity._state,
            characterEntity._position,
            characterEntity._speed,
            characterEntity._totalDistance
        ));
    }

    public void SetDto(CharacterDto dto)
    {
        _dto = dto;
    }

    private void Awake()
    {
        _init_position = transform.position;
    }

    private void Start()
    {
        // must be loaded AFTER PuzzleController Awake()
        _layerObstacle = LayerMask.GetMask("Obstacle");
        _layerArrow = LayerMask.GetMask("Arrow");
        _layerExit = LayerMask.GetMask("Exit");
        var frequency = 1/_speed;
        InvokeRepeating("Moveloop", 1, frequency);
    }

    private void OnDestroy()
    {
        CancelInvoke("Moveloop");
    }

    private void Moveloop() {
        if (CollisionWithExit())
        {
            var level = GetComponentInParent<LevelController>();
            var nextLevelNumber = level._dto._number + 1;
            _container.Resolve<FinishLevel>().Execute(new FinishLevelCommand(level._dto._id));
            _container.Resolve<SaveGame>().Execute(
                new UpdateGameCommand(
                    nextLevelNumber,
                    level._dto._id
                )
            );
            SceneManager.LoadScene("Level"+ nextLevelNumber, LoadSceneMode.Single);
        }
        else if (CollisionWithArrow())
        {
            ICharacterEntity characterEntity = _container.Resolve<UpdateCharacterDirection>().Execute(new UpdateCharacterDirectionCommand(_dto._id, _dto._direction));
            var dto = CharacterDto.Create(
                characterEntity._id,
                characterEntity._type,
                characterEntity._direction,
                characterEntity._state,
                characterEntity._position,
                characterEntity._speed,
                characterEntity._totalDistance
            );
            SetDto(dto);
            transform.rotation = Quaternion.Euler(dto._orientation);
        }
        else if (CollisionWithObstacle())
        {
            ICharacterEntity characterEntity = _container.Resolve<TurnRight>().Execute(new TurnRightCommand(_dto._id));
            var dto = CharacterDto.Create(
               characterEntity._id,
               characterEntity._type,
               characterEntity._direction,
               characterEntity._state,
               characterEntity._position,
               characterEntity._speed,
               characterEntity._totalDistance
            );
            SetDto(dto);
            transform.rotation = Quaternion.Euler(dto._orientation);
        }
        else
        {
            if (_dto != null && _dto._state == EnumCharacterState.MOVING)
            {
                VOPosition newPositionVO = _container.Resolve<MoveOnceCharacter>().Execute(new MoveOnceCharacterCommand(_dto._id));
                Vector3 newPosition = new Vector3(newPositionVO.Value.X, PuzzleController.MIN.y, newPositionVO.Value.Z);
                transform.position = newPosition;
            }
        }
    }

    private bool CollisionWithObstacle()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, 0.05f, 0), transform.forward);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction);
        if (Physics.Raycast(ray, out hit, 0.6f, _layerObstacle))
        {
            return true;
        }
        return false;
    }

    private bool CollisionWithArrow()
    {
        Ray ray = new Ray(transform.position, transform.up);
        RaycastHit hit;
        //Debug.DrawRay(ray.origin, ray.direction);
        if (Physics.Raycast(ray, out hit, 1f, _layerArrow))
        {
            TileController controller = hit.collider.GetComponentInParent<TileController>();
            if (controller._dto._arrow._direction != _dto._direction)
            {
                _dto.UpdateDirection(controller._dto._arrow._direction);
                return true;
            }
        }
        return false;
    }

    private bool CollisionWithExit()
    {
        Ray ray = new Ray(transform.position, transform.up);
        RaycastHit hit;
        //Debug.DrawRay(ray.origin, ray.direction);
        if (Physics.Raycast(ray, out hit, 1f, _layerExit))
        {
            return true;
        }
        return false;
    }

}
