using System;
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
            var (domainEventNotification, eventLabel) = CreateDomainEventNotification(aggregateRoot);
            Debug.Log("______" + eventLabel + "_____published");
            _mediator.Publish(domainEventNotification);
            aggregateRoot.ClearDomainEvents();
        }

        public (IMulticastMessage, string) CreateDomainEventNotification(IAggregateRoot aggregateRoot)
        {
            var domainEvent = aggregateRoot.DomainEvents[0];
            var domainEventType = domainEvent.GetType();
            var genericDispatcherType = typeof(DomainEventNotification<>).MakeGenericType(domainEventType);
            var notification = Activator.CreateInstance(genericDispatcherType, domainEvent);
            return ((IMulticastMessage, string))(notification, domainEvent._label);
        }

    }
}
