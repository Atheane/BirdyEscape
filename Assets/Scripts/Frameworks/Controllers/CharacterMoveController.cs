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
                (transform.position[0], transform.position[1],
                transform.position[2]),
            _speed));
        _dto = CharacterDto.Create(
            characterEntity._id,
            characterEntity._type,
            characterEntity._direction,
            new Vector3(characterEntity._position.Value.X, Position.INIT_Y, characterEntity._position.Value.Z),
            characterEntity._speed);

    }

    private void Update()
    {
        if (ShouldUpdateDirection())
        {
            _container.Resolve<UpdateDirection>().Execute(new UpdateDirectionCommand(_dto._id, _direction));
        }
        if (ValidMove())
        {
            VOPosition newPositionVO = _container.Resolve<GetCharacterPositionUsecase>().Execute(new GetCharacterPositionQuery(_dto._id));
            Vector3 newPosition = new Vector3(newPositionVO.Value.X, Position.INIT_Y, newPositionVO.Value.Z);
            transform.position = newPosition;
        } else
        {
            _direction = _container.Resolve<TurnRight>().Execute(new TurnRightCommand(_dto._id));
        }

    }

    private bool ValidMove()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, 0.25f, 0), 0.75f*transform.forward);
        RaycastHit hit;
        //Debug.DrawRay(ray.origin, ray.direction);
        if (Physics.Raycast(ray, out hit, 1f, _layerObstacle))
        {
            return false;
        }
        return true;
    }

    private bool ShouldUpdateDirection()
    {
        Ray ray = new Ray(transform.position, transform.up);
        RaycastHit hit;
        //Debug.DrawRay(ray.origin, ray.direction);
        if (Physics.Raycast(ray, out hit, 1f, _layerArrow))
        {
            EnumDirection direction = hit.collider.GetComponent<ArrowController>()._direction;
            if (direction != _direction)
            {
                _direction = direction;
                return true;
            }
        }
        return false;
    }
}
