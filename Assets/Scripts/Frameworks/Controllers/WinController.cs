using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UniMediator;
using Zenject;
using Adapters.Unimediatr;
using Domain.DomainEvents;
using Domain.Entities;
using Usecases;
using Usecases.Commands;


public enum WinState { SHOWN, HIDDEN };

public class WinController : MonoBehaviour, IMulticastMessageHandler<DomainEventNotification<GameLevelCompleted>>
{
    public WinState _state;
    public IGameEntity _gameEntity;

    private DiContainer _container;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    private void Start()
    {
        _state = WinState.HIDDEN;
        gameObject.SetActive(false);

    }

    public void Handle(DomainEventNotification<GameLevelCompleted> notification)
    {
        Debug.Log("______" + notification._domainEvent._label + "_____handled");
        _state = WinState.SHOWN;
        gameObject.SetActive(true);
        _gameEntity = notification._domainEvent._props;
        var energy = _gameEntity._energy.Value;
        var slider = gameObject.GetComponentInChildren<Slider>();
        slider.value = energy;
        var text = GameObject.FindWithTag("EnergyStatus").GetComponent<TextMeshProUGUI>();
        text.text = Mathf.RoundToInt(energy).ToString() + "/100";
    }

    public void OnClickButtonHome()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void OnClickButtonRestart()
    {
        var levelNumber = _gameEntity._currentLevel._number.ToString();
        //_container.Resolve<RestartLevel>().Execute(new RestartLevelCommand(_gameEntity._currentLevel._id));
        SceneManager.LoadScene("Level" + levelNumber, LoadSceneMode.Single);
    }

    public void OnClickButtonNext()
    {
        var nextLevelNumber = (_gameEntity._currentLevel._number + 1).ToString();
        SceneManager.LoadScene("Level" + nextLevelNumber, LoadSceneMode.Single);
    }

}
