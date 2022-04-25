using UnityEngine;
using UnityEngine.UI;
using UniMediator;
using Zenject;
using Adapters.Unimediatr;
using Domain.DomainEvents;
using Adapters.IORepository;
using Frameworks.IO;


public class EnergyBarController : MonoBehaviour, IMulticastMessageHandler<DomainEventNotification<GameEnergyComputed>>
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
        Debug.Log("EnergyBarController START");
        GameIO gameIO = _container.Resolve<IOGameRepository>().PreLoad();
        _slider = gameObject.GetComponent<Slider>();
        _slider.value = gameIO._energy / 100;
        Debug.Log(_slider.value);
    }

    public void Handle(DomainEventNotification<GameEnergyComputed> notification)
    {
        Debug.Log("______" + notification._domainEvent._label + "_____handled");
        var energy = notification._domainEvent._props._energy.Value;
        _slider.value = energy /100;
        Debug.Log(_slider.value);
    }
}
