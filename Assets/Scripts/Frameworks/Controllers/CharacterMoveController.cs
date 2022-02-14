using UnityEngine;
using Zenject;
using System;
using Usecases;
using Usecases.Queries;
using Usecases.Commands;
using Domain.ValueObjects;
using Domain.Constants;
using Domain.Types;
using Domain.Entities;


public class CharacterMoveController : MonoBehaviour
{
    public Guid _id;
    private DiContainer _container;
    public LayerMask _layer;
    public EnumDirection _direction;
    public int _speed;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    private void Awake()
    {
        ICharacterEntity characterEntity = _container.Resolve<CreateCharacter>().Execute(new CreateCharacterCommand(EnumCharacterType.BLACK_BIRD, _direction, (transform.position[0], transform.position[1], transform.position[2]), _speed));
        _id = characterEntity.Id;
        _layer = LayerMask.GetMask("Obstacle");
    }

    private void Update()
    {
        if (ShouldUpdateDirection().Item1)
        {
            _container.Resolve<UpdateDirection>().Execute(new UpdateDirectionCommand(_id, ShouldUpdateDirection().Item2));
        }
        if (ValidMove())
        {
            VOPosition3D newPositionVO = _container.Resolve<GetCharacterPositionUsecase>().Execute(new GetCharacterPositionQuery(_id));
            Vector3 newPosition = new Vector3(newPositionVO.Value.X, Position3D.INIT_Y, newPositionVO.Value.Z);
            transform.position = newPosition;
        } else
        {
            _direction = _container.Resolve<TurnRight>().Execute(new TurnRightCommand(_id));
        }

    }

    private bool ValidMove()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, 0.25f, 0), transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1.1f, _layer))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                return false;
            }
        }
        return true;
    }

    private (bool, EnumDirection) ShouldUpdateDirection()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, 0.25f, 0), -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 0.5f))
        {
            if (hit.collider.CompareTag("Arrow"))
            {
                EnumDirection direction = hit.collider.GetComponent<ArrowController>()._direction;
                if (direction != _direction)
                {
                    return (true, direction);
                }
            }
        }
        return (false, _direction);
    }
}
