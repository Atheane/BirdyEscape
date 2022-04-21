using UnityEngine;
using UniMediator;
using Adapters.Unimediatr;
using Domain.DomainEvents;

public enum PopupWinState { SHOWN, HIDDEN };

public class PopupWinController : MonoBehaviour, IMulticastMessageHandler<DomainEventNotification<LevelCompleted>>, IMulticastMessageHandler<DomainEventNotification<GameEnergyComputed>>
{
    public PopupWinState _state;

    // Start is called before the first frame update
    private void Awake()
    {
        _state = PopupWinState.HIDDEN;
        gameObject.SetActive(false);
    }

    public void Handle(DomainEventNotification<LevelCompleted> notification)
    {
        Debug.Log("______" + notification._domainEvent._label + "_____handled");
        _state = PopupWinState.SHOWN;
        gameObject.SetActive(true);
    }

    public void Handle(DomainEventNotification<GameEnergyComputed> notification)
    {
        Debug.Log("______" + notification._domainEvent._label + "_____handled");
        Debug.Log("ENERGY LEVEL");
        Debug.Log(notification._domainEvent._props._energy.Value);
    }
}
