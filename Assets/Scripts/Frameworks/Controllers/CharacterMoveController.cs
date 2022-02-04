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
        _container.Resolve<MoveAlwaysCharacter>().Execute(new MoveAlwaysCharacterCommand(_id));
    }

    private void Update()
    {
        VOPosition newPositionVO = _container.Resolve<GetCharacterPositionUsecase>().Execute(new GetCharacterPositionQuery(_id));

        Vector3 newPosition = new Vector3(newPositionVO.Value.X, Position.INIT_Y, newPositionVO.Value.Z);
        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _container.Resolve<Turn90DegreesRight>().Execute(new Turn90DegreesRightCommand(_id));

    }

}
