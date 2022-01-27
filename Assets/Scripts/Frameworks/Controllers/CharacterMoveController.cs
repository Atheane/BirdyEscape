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
        Debug.Log("SetId");
        Debug.Log(id);
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
        Debug.Log("Collision");
        EnumCharacterDirection direction = EnumCharacterDirection.LEFT;
        switch (collision.tag)
        {
            case "LeftBoundary":
                Debug.Log("COLLISION LEFT");
                direction = EnumCharacterDirection.UP;
                break;
            case "UpBoundary":
                Debug.Log("COLLISION UP");
                direction = EnumCharacterDirection.RIGHT;
                break;
            case "RightBoundary":
                Debug.Log("COLLISION RIGHT");
                direction = EnumCharacterDirection.DOWN;
                break;
        }
        _container.Resolve<ChangeCharacterDirection>().Execute(new ChangeCharacterDirectionCommand(_id, direction));
    }

}
