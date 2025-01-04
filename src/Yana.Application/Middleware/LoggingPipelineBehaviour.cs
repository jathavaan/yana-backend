using Yana.Application.Common;

namespace Yana.Application.Middleware;

public class LoggingPipelineBehaviour<TRequest, TResponse>(
    ILogger<LoggingPipelineBehaviour<TRequest, TResponse>> logger
) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<LoggingPipelineBehaviour<TRequest, TResponse>> _logger = logger;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await next().ConfigureAwait(false);
            if (result is not Response response ||
                (string.IsNullOrWhiteSpace(response.ErrorMessage) && response.ErrorMessage is null))
                return result;

            var errorMessage = !string.IsNullOrWhiteSpace(response.ErrorMessage)
                ? response.ErrorMessage
                : response.ErrorCode.ToString();

            _logger.LogError(errorMessage);

            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{request.GetType().FullName} : {e.Message}", e.GetBaseException());
            throw;
        }
    }
}