using UnityEngine;
using Zenject;
using System;
using Usecases.Characters;
using Usecases.Characters.Queries;
using Usecases.Characters.Commands;
using Domain.Characters.ValueObjects;

public class CharacterMoveController : MonoBehaviour
{
    private Guid _id;
    private DiContainer _container;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    private void Start()
    {
        _container.Resolve<MoveAlwaysCharacter>().Execute(new MoveAlwaysCharacterCommand(_id));
    }

    private void Update()
    {
        Debug.Log("_______________CharacterMoveController, Update()");
        Debug.Log("_id");
        Debug.Log(_id);

        VOPosition newPositionVO = _container.Resolve<GetCharacterPositionUsecase>().Execute(new GetCharacterPositionQuery(_id));

        Vector3 newPosition = new Vector3(newPositionVO.Value.X, newPositionVO.Value.Y, 0f);
        Debug.Log(newPosition);
        transform.position = newPosition;
    }

    public void SetId(Guid id)
    {
        Debug.Log("SetId");
        Debug.Log(id);
        _id = id;
    }

}
