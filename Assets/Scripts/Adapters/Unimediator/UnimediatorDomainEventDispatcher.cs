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

        public void Dispatch(IAggregateRoot aggregateRoot)
        {
            var domainEventNotifications = CreateDomainEventNotifications(aggregateRoot);
            foreach (IMulticastMessage message in domainEventNotifications)
            {
                _mediator.Publish(message);
            }
            aggregateRoot.ClearDomainEvents();
        }

        public IReadOnlyList<IMulticastMessage> CreateDomainEventNotifications(IAggregateRoot aggregateRoot)
        {
            List<IMulticastMessage> notificationList = new List<IMulticastMessage>();
            Type domainEventType;
            Type genericDispatcherType;
            object notification;

            foreach (IDomainEvent domainEvent in aggregateRoot.DomainEvents)
            {
                Debug.Log("______" + domainEvent._label + "_____published");
                domainEventType = domainEvent.GetType();
                genericDispatcherType = typeof(DomainEventNotification<>).MakeGenericType(domainEventType);
                notification = Activator.CreateInstance(genericDispatcherType, domainEvent);
                notificationList.Add((IMulticastMessage)notification);
            }
            return notificationList;
        }

    }
}
