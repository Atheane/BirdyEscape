using UnityEngine;
using Zenject;
using System;
using Usecases.Characters;
using Usecases.Characters.Queries;
using Usecases.Characters.Commands;
using Domain.Characters.ValueObjects;
using Domain.Characters.Constants;

public class CharacterMoveController : MonoBehaviour
{
    private Guid _id;
    private DiContainer _container;
    public LayerMask _layer;

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
        if (ValidMove())
        {
            VOPosition newPositionVO = _container.Resolve<GetCharacterPositionUsecase>().Execute(new GetCharacterPositionQuery(_id));
            Vector3 newPosition = new Vector3(newPositionVO.Value.X, Position.INIT_Y, newPositionVO.Value.Z);
            transform.position = newPosition;
        } else
        {
            _container.Resolve<TurnRight>().Execute(new TurnRightCommand(_id));
        }

    }

    private bool ValidMove()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, 0.25f, 0), transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1.5f, _layer))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                return false;
            }
        }
        return true;

    }

}
