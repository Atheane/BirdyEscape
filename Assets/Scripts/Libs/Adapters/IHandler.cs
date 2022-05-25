using Libs.Usecases;

namespace Libs.Adapters
{
    public interface IHandler<IRequest, IResponse>
    {
        IUsecase<IRequest, IResponse> Usecase { get; }
        public IResponse Handle(IRequest command);
    }
}