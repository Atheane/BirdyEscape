using System.Threading.Tasks;

namespace Libs.Domain.DomainEvents
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(IDomainEvent domainEvent);
    }
}
