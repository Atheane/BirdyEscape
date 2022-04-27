using UnityEngine;
using UnityEngine.UI;
using UniMediator;
using Zenject;
using Adapters.Unimediatr;
using Adapters.IORepository;
using Domain.DomainEvents;
using Frameworks.IO;

public class EnergyBarController : MonoBehaviour,  IMulticastMessageHandler<DomainEventNotification<GameEnergyComputed>>
{
    public Slider _slider;
    private DiContainer _container;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    private void Start()
    {
        _slider.maxValue = 1.0f;
        _slider = gameObject.GetComponent<Slider>();
        GameIO gameIO = _container.Resolve<IOGameRepository>().PreLoad();
        _slider.value = gameIO._energy/100;
    }

    public void Handle(DomainEventNotification<GameEnergyComputed> notification)
    {
        Debug.Log("______" + notification._domainEvent._label + "_____handled");
        var energy = notification._domainEvent._props._energy.Value;
        _slider.value = energy /100;
    }
}
