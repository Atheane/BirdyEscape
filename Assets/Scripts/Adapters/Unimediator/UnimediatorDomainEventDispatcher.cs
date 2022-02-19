using System;
using System.Collections.Generic;
using UniMediator;
using UnityEngine;
using Libs.Domain.Entities;
using Libs.Domain.DomainEvents;

namespace Adapters.Unimediatr
{
    public class UnimediatorDomainEventDispatcher : IDomainEventDispatcher
    {
        private IMediator _mediator;

        public UnimediatorDomainEventDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public string Dispatch(IAggregateRoot aggregateRoot)
        {
            var domainEventNotifications = CreateDomainEventNotifications(aggregateRoot);
            foreach (ISingleMessage<string> message in domainEventNotifications)
            {
                _mediator.Send(message);
            }
            return "dispatched";
            aggregateRoot.ClearDomainEvents();
        }

        public IReadOnlyList<ISingleMessage<string>> CreateDomainEventNotifications(IAggregateRoot aggregateRoot)
        {
            List<ISingleMessage<string>> notificationList = new List<ISingleMessage<string>>();
            Type domainEventType;
            Type genericDispatcherType;
            object notification;

            foreach (IDomainEvent domainEvent in aggregateRoot.DomainEvents)
            {
                Debug.Log("______" + domainEvent._label + "_____published");
                domainEventType = domainEvent.GetType();
                genericDispatcherType = typeof(DomainEventNotification<>).MakeGenericType(domainEventType);
                notification = Activator.CreateInstance(genericDispatcherType, domainEvent);
                notificationList.Add((ISingleMessage<string>)notification);
            }
            return notificationList;
        }

    }
}
