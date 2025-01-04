namespace Yana.Api.Controllers;

public class UserController(IMediator mediator) : YanaControllerBase(mediator)
{
    [HttpGet("{userId}")]
    [Produces("application/json")]
    [ApiConventionMethod(typeof(SwaggerApiConvention), nameof(SwaggerApiConvention.StatusResponseTypes))]
    [ActionName(nameof(GetUserById))]
    public string GetUserById(string userId)
    {
        return userId;
    }

    [HttpGet("{email}")]
    [Produces("application/json")]
    [ApiConventionMethod(typeof(SwaggerApiConvention), nameof(SwaggerApiConvention.StatusResponseTypes))]
    [ActionName(nameof(GetUserByEmail))]
    public string GetUserByEmail(string email)
    {
        return email;
    }
}