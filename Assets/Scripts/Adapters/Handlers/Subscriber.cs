using UnityEngine;
using UniMediator;
using Adapters.Unimediatr;

public class Subscriber : MonoBehaviour, IMulticastMessageHandler<DomainEventNotification>
{
    // Start is called before the first frame update
    public void Handle(DomainEventNotification notification)
    {
        Debug.Log("Subscriber");
        Debug.Log(notification);
    }
}
