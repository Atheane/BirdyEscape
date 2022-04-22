using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Zenject;
using Usecases;
using Domain.Entities;


public enum MainMenuPlayButtonState { CLICKED, IDLE };

public class MainMenuPlayController : MonoBehaviour, IPointerDownHandler
{
    public MainMenuPlayButtonState _state;
    private DiContainer _container;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    private void Start()
    {
        _state = MainMenuPlayButtonState.IDLE;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_state == MainMenuPlayButtonState.IDLE)
        {
            _state = MainMenuPlayButtonState.CLICKED;
            try
            {
                IGameEntity gameEntity = _container.Resolve<LoadGame>().Execute(IntPtr.Zero);
                SceneManager.LoadScene("Level" + gameEntity._currentLevel._number, LoadSceneMode.Single);
            } catch(Exception e)
            {
                Debug.Log(e);
                SceneManager.LoadScene("Level1", LoadSceneMode.Single);
            }
        }
    }
}
