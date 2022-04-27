using UnityEngine;
using UniMediator;
using Adapters.Unimediatr;
using Domain.DomainEvents;


public enum PopupLoseState { SHOWN, HIDDEN };

public class PopupLoseController : MonoBehaviour, IMulticastMessageHandler<DomainEventNotification<GameOver>>
{
    public PopupWinState _state;

    // Start is called before the first frame update
    private void Awake()
    {
        _state = PopupWinState.HIDDEN;
        gameObject.SetActive(false);
    }

    public void Handle(DomainEventNotification<GameOver> notification)
    {
        Debug.Log("______" + notification._domainEvent._label + "_____handled");
        _state = PopupWinState.SHOWN;
        gameObject.SetActive(true);
    }
}
