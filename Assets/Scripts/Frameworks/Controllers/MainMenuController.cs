using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Zenject;
using Usecases;
using Domain.Entities;


public enum MainMenuButtonState { CLICKED, IDLE };

public class MainMenuController : MonoBehaviour, IPointerDownHandler
{
    public MainMenuButtonState _state;
    private DiContainer _container;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    private void Start()
    {
        _state = MainMenuButtonState.IDLE;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_state == MainMenuButtonState.IDLE)
        {
            _state = MainMenuButtonState.CLICKED;
            IGameEntity gameEntity = _container.Resolve<LoadOrCreateGame>().Execute(IntPtr.Zero);
            SceneManager.LoadScene("Level" + gameEntity._currentLevel, LoadSceneMode.Single);
        }
    }
}
