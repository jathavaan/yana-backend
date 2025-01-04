using Yana.Application.Common;

namespace Yana.Application.Middleware;

public class ErrorHandlingPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : Response, new()
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next().ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return new TResponse
            {
                ErrorMessage = e.Message,
                ErrorCode = ErrorCode.Unknown
            };
        }
    }
}