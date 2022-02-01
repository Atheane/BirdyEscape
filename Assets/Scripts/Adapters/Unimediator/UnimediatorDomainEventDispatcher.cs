using System;
using System.Collections.Generic;
using UniMediator;
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
            var domainEventNotification = CreateDomainEventNotification(aggregateRoot);
            _mediator.Publish(domainEventNotification);
        }

        public IMulticastMessage CreateDomainEventNotification(IAggregateRoot aggregateRoot)
        {
            IDomainEvent domainEvent = aggregateRoot.DomainEvents[0];
            //var domainEventType = domainEvent.GetType();
            //var genericDispatcherType = typeof(DomainEventNotification<>).MakeGenericType(domainEventType);
            var notification = Activator.CreateInstance(typeof(DomainEventNotification), domainEvent);
            return (IMulticastMessage)notification;
        }

    }
}
