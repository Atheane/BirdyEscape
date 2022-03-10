using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UniMediator;
using Zenject;
using Adapters.Unimediatr;
using Usecases;
using Usecases.Commands;
using Domain.Entities;
using Domain.Types;
using Domain.DomainEvents;

public enum EnumButtonState { PLAY, RESTART, HIDDEN };

public class PlayButtonController : MonoBehaviour, IPointerDownHandler, IMulticastMessageHandler<DomainEventNotification<TileArrowAdded>>
{
    public Sprite _spriteButtonOff;
    public EnumButtonState _state;
    private DiContainer _container;
    private IReadOnlyList<ICharacterEntity> _characters;
    private Image _icon;
    private Sprite _spriteButtonOn;
    private Image _button;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    public void Handle(DomainEventNotification<TileArrowAdded> notification)
    {
        Debug.Log("______" + notification._domainEvent._label + "_____handled");
        _button.enabled = true;
        _icon.enabled = true;
        _state = EnumButtonState.PLAY;
        _icon.sprite = _spriteButtonOn;
    }

    private void Start()
    {
        _state = EnumButtonState.HIDDEN;
        _icon = transform.GetChild(0).GetComponent<Image>();
        _button = GetComponent<Image>();
        _spriteButtonOn = _icon.sprite;
        _characters = _container.Resolve<GetAllCharacters>().Execute(IntPtr.Zero);
        _button.enabled = false;
        _icon.enabled = false;
    }

    private void Update()
    {
        if (_state == EnumButtonState.HIDDEN)
        {
            _button.enabled = false;
            _icon.enabled = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        var levelController = transform.GetComponentInParent<LevelController>();

        if (_state == EnumButtonState.PLAY)
        {
            _icon.sprite = _spriteButtonOff;
            _state = EnumButtonState.RESTART;

            _container.Resolve<UpdateLevelState>().Execute(new UpdateLevelStateCommand(levelController._dto._id, EnumLevelState.ON));

            foreach (ICharacterEntity character in _characters)
            {
                if (character._state == EnumCharacterState.IDLE)
                {
                    _container.Resolve<UpdateCharacterState>().Execute(new UpdateCharacterStateCommand(character._id, EnumCharacterState.MOVING));
                }
            }
        } else
        {
            (Guid id, Vector3 position, EnumDirection direction)[] charactersRestartProps = levelController.GetCharactersInit();
            Guid[] tilesIds = levelController.GetTilesIdsWithArrows(); 
            _container.Resolve<RestartLevel>().Execute(
                new RestartLevelCommand(
                    levelController._dto._id,
                    charactersRestartProps,
                    tilesIds
                ));
            _state = EnumButtonState.HIDDEN;
            _icon.sprite = _spriteButtonOn;
        }
    }

}
