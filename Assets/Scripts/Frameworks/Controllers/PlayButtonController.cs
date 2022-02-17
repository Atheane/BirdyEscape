using System;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using UnityEngine.EventSystems;
using Usecases;
using Usecases.Commands;
using Domain.Entities;
using Domain.Types;

public class PlayButtonController : MonoBehaviour, IPointerDownHandler
{
    private DiContainer _container;
    private IReadOnlyList<ICharacterEntity> _characters;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    void Start()
    {
        _characters = _container.Resolve<GetAllCharacters>().Execute(IntPtr.Zero);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        foreach (ICharacterEntity character in _characters)
        {
            if (character._state == EnumCharacterState.IDLE)
            {
                _container.Resolve<MoveAlwaysCharacter>().Execute(new MoveAlwaysCharacterCommand(character._id));
            }
        }
    }

}
