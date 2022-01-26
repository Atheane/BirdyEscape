using UnityEngine;
using Zenject;
using System;
using Usecases.Characters;
using Usecases.Characters.Queries;
using Usecases.Characters.Commands;
using Domain.Characters.Repositories;
using Domain.Characters.Entities;
using Domain.Characters.ValueObjects;

public class CharacterMoveController : MonoBehaviour
{
    private Guid _id;
    private DiContainer _container;
    private ICharacterEntity _characterEntity;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    private void Start()
    {
        _characterEntity = _container.Resolve<ICharactersRepository>().Find(_id);
        _container.Resolve<MoveAlwaysCharacter>().Execute(new MoveAlwaysCharacterCommand(_id));
    }

    private void Update()
    {
        Debug.Log("_______________-");
        VOPosition newPositionVO = _container.Resolve<GetCharacterPosition>().Execute(new GetCharacterPositionQuery(_id));

        Vector3 newPosition = new Vector3(newPositionVO.Value.X, newPositionVO.Value.Y, 0f);
        Debug.Log(newPosition);
        transform.position = newPosition;
    }

    public void SetId(Guid id)
    {
        _id = id;
    }

}
