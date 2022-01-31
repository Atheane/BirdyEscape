using System.Threading.Tasks;
using Libs.Domain.Entities;

namespace Libs.Domain.DomainEvents
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(IAggregateRoot aggregateRoot);
    }
}
