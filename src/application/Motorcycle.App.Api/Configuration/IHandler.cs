using MediatR;
using motorcycle.app.api.Configuration;

namespace mmotorcycle.app.api.Configuration
{
    public interface IHandler<TInput, TOutput>
       where TInput : IRequest<Response<TOutput>>
       where TOutput : class
    {
        Task<Response<TOutput>> Handle(TInput input, CancellationToken cancellation);
    }
}
