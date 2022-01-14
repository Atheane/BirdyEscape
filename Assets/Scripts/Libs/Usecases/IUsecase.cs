using Libs.Domain.DomainEvents;

//IRequest is either a command or a query
namespace Libs.Usecases
{
    public interface IUsecase<IRequest, IResult>
    {
        IResult Execute(IRequest request);
    }
}

