using UnityEngine;
using Zenject;
using UniMediator;
using Domain.DomainEvents;
using Domain.Entities;
using Adapters.Unimediatr;
using Frameworks.Dtos;

public class ArrowCreatedHandler : MonoBehaviour, IMulticastMessageHandler<DomainEventNotification<ArrowCreatedDomainEvent>>
{
    private DiContainer _container;

    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }

    public void Handle(DomainEventNotification<ArrowCreatedDomainEvent> notification)
    {
        Debug.Log("______" + notification._domainEvent._label + "_____handled");
        IArrowEntity arrowEntity = notification._domainEvent._props;
        IArrowDto dto = ArrowDto.Create(
            arrowEntity._id,
            arrowEntity._direction,
            arrowEntity._coordinates,
            arrowEntity._path
        );
        DrawArrow(dto);
    }

    public void DrawArrow(IArrowDto dto)
    {
        Transform grid = transform.parent;
        GameObject go = Instantiate(Resources.Load(dto._path), dto._position, Quaternion.Euler(dto._orientation)) as GameObject;
        // instantiate and attach the component in once function
        var controller = _container.InstantiateComponent<ArrowController>(go);
        controller._dto = dto;
        go.transform.parent = grid;
        go.tag = Entities.Arrow.ToString();
    }
}
