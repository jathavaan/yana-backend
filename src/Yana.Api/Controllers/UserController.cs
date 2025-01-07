using Microsoft.AspNetCore.Authorization;

namespace Yana.Api.Controllers;

public class UserController(IMediator mediator) : YanaControllerBase(mediator)
{
    [HttpGet("id/{userId}")]
    [Produces("application/json")]
    [ApiConventionMethod(typeof(SwaggerApiConvention), nameof(SwaggerApiConvention.StatusResponseTypes))]
    [ActionName(nameof(GetUserById))]
    public string GetUserById(string userId)
    {
        return userId;
    }

    [HttpGet("email/{email}")]
    [Produces("application/json")]
    [ApiConventionMethod(typeof(SwaggerApiConvention), nameof(SwaggerApiConvention.StatusResponseTypes))]
    [ActionName(nameof(GetUserByEmail))]
    [Authorize]
    public string GetUserByEmail(string email)
    {
        return email;
    }
}