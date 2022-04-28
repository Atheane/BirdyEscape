using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UniMediator;
using Adapters.Unimediatr;
using Domain.DomainEvents;
using Domain.Entities;

public enum WinState { SHOWN, HIDDEN };

public class WinController : MonoBehaviour, IMulticastMessageHandler<DomainEventNotification<GameLevelCompleted>>
{
    public WinState _state = WinState.HIDDEN;
    public int _currentLevelNumber;

    private void Start()
    {
        if (_state == WinState.HIDDEN)
        {
            gameObject.SetActive(false);
        }
    }

    public void Handle(DomainEventNotification<GameLevelCompleted> notification)
    {
        Debug.Log("______" + notification._domainEvent._label + "_____handled");
        _state = WinState.SHOWN;
        gameObject.SetActive(true);
        _currentLevelNumber = notification._domainEvent._props._currentLevel._number;
        var remainingEnergy = notification._domainEvent._props._energy.Value;
        float energyUsed = 0f;
        foreach (ICharacterEntity character in notification._domainEvent._props._currentLevel._characters)
        {
            energyUsed += character._totalDistance;
        }
        // update text with energy statistics
        var remainingEnergyUI = GameObject.FindWithTag("RemainingEnergy").GetComponent<TextMeshProUGUI>();
        remainingEnergyUI.text = Mathf.RoundToInt(remainingEnergy).ToString() + "/100";
        // update slider value
        var slider = gameObject.GetComponentInChildren<Slider>();
        slider.value = remainingEnergy;
        // update energy used
        var energyUsedUI = GameObject.FindWithTag("EnergyUsed").GetComponent<TextMeshProUGUI>();
        energyUsedUI.text = Mathf.RoundToInt(energyUsed).ToString();
    }

    public void OnClickButtonHome()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void OnClickButtonRestart()
    {
        SceneManager.LoadScene("Level" + _currentLevelNumber.ToString(), LoadSceneMode.Single);
    }

    public void OnClickButtonNext()
    {
        var nextLevelNumber = (_currentLevelNumber + 1).ToString();
        SceneManager.LoadScene("Level" + nextLevelNumber, LoadSceneMode.Single);
    }

}
