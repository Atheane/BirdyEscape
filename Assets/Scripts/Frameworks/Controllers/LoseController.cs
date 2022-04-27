using UnityEngine;
using UniMediator;
using Adapters.Unimediatr;
using Domain.DomainEvents;


public enum LoseState { SHOWN, HIDDEN };

public class LoseController : MonoBehaviour, IMulticastMessageHandler<DomainEventNotification<GameOver>>
{
    public LoseState _state;

    // Start is called before the first frame update
    private void Awake()
    {
        _state = LoseState.HIDDEN;
        gameObject.SetActive(false);
    }

    public void Handle(DomainEventNotification<GameOver> notification)
    {
        Debug.Log("______" + notification._domainEvent._label + "_____handled");
        _state = LoseState.SHOWN;
        gameObject.SetActive(true);
        Debug.Log(notification._domainEvent._props._energy);
    }
}
