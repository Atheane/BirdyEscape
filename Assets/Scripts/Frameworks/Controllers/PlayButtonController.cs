using System;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Usecases;
using Usecases.Commands;
using Domain.Entities;
using Domain.Types;

public enum EnumButtonState { ON, OFF };

public class PlayButtonController : MonoBehaviour, IPointerDownHandler
{
    public Sprite _spriteButtonOff;
    public EnumButtonState _state;
    private DiContainer _container;
    private IReadOnlyList<ICharacterEntity> _characters;
    private Image _icon;
    private Sprite _spriteButtonOn;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    void Start()
    {
        _icon = transform.GetChild(0).GetComponent<Image>();
        _spriteButtonOn = _icon.sprite;
        _characters = _container.Resolve<GetAllCharacters>().Execute(IntPtr.Zero);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_state == EnumButtonState.OFF)
        {
            _icon.sprite = _spriteButtonOff;
            _state = EnumButtonState.ON;
            foreach (ICharacterEntity character in _characters)
            {
                if (character._state == EnumCharacterState.IDLE)
                {
                    _container.Resolve<UpdateCharacterState>().Execute(new UpdateCharacterStateCommand(character._id, EnumCharacterState.MOVING));
                    _state = EnumButtonState.ON;
                    //todo should be saved in Level Aggregate root
                }
            }
        } else
        {
            Debug.Log("SHOULD RESTART");
            // Level.Restart() => publish LEVEL_RESTARTED
            _state = EnumButtonState.OFF;
            _icon.sprite = _spriteButtonOn;
        }
    }

}
