using UnityEngine;
using Zenject;
using System;
using Usecases.Characters;
using Usecases.Characters.Queries;
using Usecases.Characters.Commands;
using Domain.Characters.ValueObjects;
using Domain.Characters.Types;

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

        Vector3 newPosition = new Vector3(newPositionVO.Value.X, newPositionVO.Value.Y, 0f);
        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnumCharacterDirection direction = EnumCharacterDirection.LEFT;
        switch (collision.tag)
        {
            case "LeftBoundary":
                direction = EnumCharacterDirection.UP;
                break;
            case "UpBoundary":
                direction = EnumCharacterDirection.RIGHT;
                break;
            case "RightBoundary":
                direction = EnumCharacterDirection.DOWN;
                break;
        }
        _container.Resolve<ChangeCharacterDirection>().Execute(new ChangeCharacterDirectionCommand(_id, direction));
    }

}
