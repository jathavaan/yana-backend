using Yana.Application.Common;

namespace Yana.Api.Base;

[Route("api/[controller]s")]
[ApiConventionType(typeof(SwaggerApiConvention))]
[ApiController]
public abstract class YanaControllerBase(IMediator mediator) : ControllerBase
{
    internal async Task<ActionResult<TResult>> SendQuery<TResult, TRequest>(TRequest? query)
        where TRequest : Request<Response<TResult>>
    {
        if (query is null) return BadRequest();

        var response = await mediator.Send(query);
        return !string.IsNullOrWhiteSpace(response.ErrorMessage) || response.ErrorCode is not null
            ? GetErrorResult(response)
            : Ok(response.Result);
    }

    internal async Task<ActionResult<TResult>> SendCommand<TResult, TRequest>(TRequest? command)
        where TRequest : Command<CommandResponse<TResult>>
    {
        if (command is null) return BadRequest();

        var response = await mediator.Send(command);
        return !string.IsNullOrWhiteSpace(response.ErrorMessage) || response.ErrorCode is not null
            ? GetErrorResult(response)
            : Ok(response.Result);
    }

    private ActionResult GetErrorResult(Response result)
    {
        return result.ErrorCode switch
        {
            ErrorCode.AlreadyExists => Problem(result.ErrorMessage),
            ErrorCode.Forbidden => Forbid(result.ErrorMessage!),
            ErrorCode.NotFound => NotFound(result.ErrorMessage),
            _ => Problem(result.ErrorMessage)
        };
    }
}