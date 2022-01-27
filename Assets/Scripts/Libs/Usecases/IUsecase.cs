using Libs.Domain.DomainEvents;
using Libs.Domain.Entities;

//IRequest is either a command or a query
namespace Libs.Usecases
{
    public interface IUsecase<IRequest, IResponse>
    {
        IResponse Execute(IRequest request);
    }
}
