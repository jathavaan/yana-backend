namespace Yana.Api.Controllers;

public class UserController(IMediator mediator) : YanaControllerBase(mediator)
{
    [HttpGet("id/{userId}")]
    [Produces("application/json")]
    [ApiConventionMethod(typeof(SwaggerApiConvention), nameof(SwaggerApiConvention.StatusResponseTypes))]
    [ActionName(nameof(GetUserById))]
    [AuthorizeUser]
    public async Task<ActionResult<UserVm>> GetUserById(string userId)
        => await SendQuery<UserVm, GetUserByIdQuery>(new GetUserByIdQuery(userId));


}