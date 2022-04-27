using UnityEngine;
using UnityEngine.UI;
using UniMediator;
using Adapters.Unimediatr;
using Domain.DomainEvents;
using UnityEngine.SceneManagement;

public enum PopupWinState { SHOWN, HIDDEN };

public class PopupWinController : MonoBehaviour, IMulticastMessageHandler<DomainEventNotification<GameLevelCompleted>>
{
    public PopupWinState _state;

    // Start is called before the first frame update
    private void Awake()
    {
        _state = PopupWinState.HIDDEN;
        gameObject.SetActive(false);
    }

    public void Handle(DomainEventNotification<GameLevelCompleted> notification)
    {
        Debug.Log("______" + notification._domainEvent._label + "_____handled");
        _state = PopupWinState.SHOWN;
        var energy = notification._domainEvent._props._energy.Value;
        gameObject.SetActive(true);
        GameObject.FindWithTag("EnergyStatistics").GetComponent<Text>().text = energy.ToString() + "/ 100";
        gameObject.GetComponentInChildren<Slider>().value = energy / 100;
    }
}
