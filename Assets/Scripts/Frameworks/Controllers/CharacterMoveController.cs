using UnityEngine;
using Zenject;
using System;
using Usecases.Characters;
using Usecases.Characters.Queries;
using Usecases.Characters.Commands;
using Domain.Characters.ValueObjects;
using Domain.Characters.Constants;
using Domain.Commons.Types;


public class CharacterMoveController : MonoBehaviour
{
    public Guid _id;
    private DiContainer _container;
    public LayerMask _layer;
    public EnumDirection _direction;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    public void SetId(Guid id)
    {
        _id = id;
    }

    private void Start()
    {
        _layer = LayerMask.GetMask("Obstacle");
        _container.Resolve<MoveAlwaysCharacter>().Execute(new MoveAlwaysCharacterCommand(_id));
    }

    private void Update()
    {
        if (ShouldUpdateDirection().Item1)
        {
            _container.Resolve<UpdateDirection>().Execute(new UpdateDirectionCommand(_id, ShouldUpdateDirection().Item2));
        }
        if (ValidMove())
        {
            VOPosition newPositionVO = _container.Resolve<GetCharacterPositionUsecase>().Execute(new GetCharacterPositionQuery(_id));
            Vector3 newPosition = new Vector3(newPositionVO.Value.X, Position.INIT_Y, newPositionVO.Value.Z);
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
        if (Physics.Raycast(ray, out hit, 1.2f))
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
