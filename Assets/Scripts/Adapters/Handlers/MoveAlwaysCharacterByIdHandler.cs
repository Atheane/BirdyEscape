﻿using UnityEngine;
using UniMediator;
using Zenject;
using Usecases.Characters;
using Usecases.Characters.Commands;
using Domain.Characters.ValueObjects;

public class MoveAlwaysCharacterByIdHandler : MonoBehaviour,
ISingleMessageHandler<MoveAlwaysCharacterCommand, VOPosition>
{
    private MoveAlwaysCharacter _usecase;

    [Inject]
    public void Construct(MoveAlwaysCharacter usecase)
    {
        _usecase = usecase;
    }

    public VOPosition Handle(MoveAlwaysCharacterCommand message)
    {
        var position = _usecase.Execute(message);
        MoveCharacter(new Vector3(position.Value.X, position.Value.Y, 0));
        return position;
    }

    public void MoveCharacter(Vector3 position)
    {
        GameObject cow = GameObject.FindGameObjectWithTag("Cow");
        cow.transform.position = position;
    }
}