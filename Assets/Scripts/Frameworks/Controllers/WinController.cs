using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UniMediator;
using Adapters.Unimediatr;
using Domain.DomainEvents;

public enum WinState { SHOWN, HIDDEN };

public class WinController : MonoBehaviour, IMulticastMessageHandler<DomainEventNotification<GameLevelCompleted>>
{
    public WinState _state;

    // Start is called before the first frame update
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
        var energy = notification._domainEvent._props._energy.Value;
        var slider = gameObject.GetComponentInChildren<Slider>();
        slider.value = energy / 100;
        var text = GameObject.FindWithTag("EnergyStatus").GetComponent<TextMeshProUGUI>();
        text.text = Mathf.RoundToInt(energy).ToString() + "/100";
        //SceneManager.LoadScene("Level" + nextLevelNumber, LoadSceneMode.Single);
    }

}
