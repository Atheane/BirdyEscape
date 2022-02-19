using UnityEngine;
using Zenject;
using UniMediator;
using Domain.DomainEvents;
using Domain.Entities;
using Adapters.Unimediatr;
using Frameworks.Dtos;

public class ArrowCreatedHandler : MonoBehaviour, ISingleMessageHandler<DomainEventNotification<ArrowCreatedDomainEvent>, string>
{
    public ArrowDto _dto;
    private DiContainer _container;


    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    public string Handle(DomainEventNotification<ArrowCreatedDomainEvent> notification)
    {
        string log = "______" + notification._domainEvent._label + "_____handled";
        Debug.Log(log);
        IArrowEntity arrowEntity = notification._domainEvent._props;
        _dto = ArrowDto.Create(
            arrowEntity._id,
            arrowEntity._direction,
            arrowEntity._coordinates,
            arrowEntity._path
        );
        DrawArrow();
        return log;
    }

    public void DrawArrow()
    {
        Transform grid = transform.parent;
        GameObject go = Instantiate(Resources.Load(_dto._path), _dto._position, Quaternion.Euler(_dto._orientation)) as GameObject;
        // instantiate and attach the component in once function
        var controller = _container.InstantiateComponent<ArrowController>(go);
        controller._direction = _dto._direction;
        go.transform.parent = grid;
    }
}
