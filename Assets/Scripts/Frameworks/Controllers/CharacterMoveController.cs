using System;
using UnityEngine;
using Zenject;
using Usecases;
using Usecases.Queries;
using Usecases.Commands;
using Domain.ValueObjects;
using Domain.Constants;
using Domain.Types;
using Domain.Entities;
using Frameworks.Dtos;

public class CharacterMoveController : MonoBehaviour
{
    public CharacterDto _dto;

    [SerializeField] private EnumDirection _direction;
    [SerializeField] private int _speed;

    private LayerMask _layerObstacle;
    private LayerMask _layerArrow;

    private DiContainer _container;

    float timer = 0;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    private void Awake()
    {
        _layerObstacle = LayerMask.GetMask("Obstacle");
        _layerArrow = LayerMask.GetMask("Arrow");
        ICharacterEntity characterEntity = _container.Resolve<CreateCharacter>().Execute(
            new CreateCharacterCommand(
                EnumCharacterType.BLACK_BIRD,
                _direction,
                transform.position,
                _speed)
            );
        _dto = CharacterDto.Create(
            characterEntity._id,
            characterEntity._type,
            characterEntity._direction,
            characterEntity._position,
            characterEntity._speed
        );
    }

    private void Update()
    {
        timer += Time.deltaTime * 1000;

        if (timer > _dto._speed)
        {
            Moveloop();
            timer = 0;
        }

    }

    private void Moveloop() {

        if (CollisionWithArrow())
        {
            _container.Resolve<UpdateCharacterDirection>().Execute(new UpdateCharacterDirectionCommand(_dto._id, _direction));
        }
        else if (CollisionWithObstacle())
        {
            _direction = _container.Resolve<TurnRight>().Execute(new TurnRightCommand(_dto._id));
        }
        else
        {
            var state = _container.Resolve<GetCharacterState>().Execute(new GetCharacterStateQuery(_dto._id));
            if (state == EnumCharacterState.MOVING)
            {
                VOPosition newPositionVO = _container.Resolve<MoveOnceCharacter>().Execute(new MoveOnceCharacterCommand(_dto._id));
                Vector3 newPosition = new Vector3(newPositionVO.Value.X, Position.INIT_Y, newPositionVO.Value.Z);
                transform.position = newPosition;
            }
        }
    }

    private bool CollisionWithObstacle()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, 0.25f, 0), transform.forward);
        RaycastHit hit;
        //Debug.DrawRay(ray.origin, ray.direction);
        if (Physics.Raycast(ray, out hit, 0.5f, _layerObstacle))
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
            ArrowController controller = hit.collider.GetComponent<ArrowController>();
            if (controller._dto._direction != _direction)
            {
                _direction = controller._dto._direction;
                return true;
            }
        }
        return false;
    }
}
