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
    public int _currentLevelNumber;

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
        _currentLevelNumber = notification._domainEvent._props._currentLevel._number;
        var energy = notification._domainEvent._props._energy.Value;
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
        SceneManager.LoadScene("Level" + _currentLevelNumber.ToString(), LoadSceneMode.Single);
    }

    public void OnClickButtonNext()
    {
        var nextLevelNumber = (_currentLevelNumber + 1).ToString();
        SceneManager.LoadScene("Level" + nextLevelNumber, LoadSceneMode.Single);
    }

}
