using System;
using System.Threading.Tasks;
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

        public Task Dispatch(IAggregateRoot aggregateRoot)
        {
            var domainEventNotification = CreateDomainEventNotification(aggregateRoot);
            try
            {

                _mediator.Send(domainEventNotification);
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return Task.FromException(e);
            }

        }

        public ISingleMessage<Task> CreateDomainEventNotification(IAggregateRoot aggregateRoot)
        {
            IDomainEvent domainEvent = aggregateRoot.DomainEvents[0];
            var domainEventType = domainEvent.GetType();
            var genericDispatcherType = typeof(DomainEventNotification<>).MakeGenericType(domainEventType);
            var notification = Activator.CreateInstance(genericDispatcherType, domainEvent);
            return (ISingleMessage<Task>)notification;
        }

    }
}
