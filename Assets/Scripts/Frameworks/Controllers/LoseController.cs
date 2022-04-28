using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UniMediator;
using Adapters.Unimediatr;
using Domain.DomainEvents;


public enum LoseState { SHOWN, HIDDEN };

public class LoseController : MonoBehaviour, IMulticastMessageHandler<DomainEventNotification<GameOver>>
{
    public LoseState _state = LoseState.HIDDEN;
    public int _currentLevelNumber;

    // Start is called before the first frame update
    private void Awake()
    {
        Debug.Log("AWAKE");
        if (_state == LoseState.HIDDEN)
        {
            gameObject.SetActive(false);
        }
    }

    public void Handle(DomainEventNotification<GameOver> notification)
    {
        Debug.Log("______" + notification._domainEvent._label + "_____handled");
        _state = LoseState.SHOWN;
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
}
