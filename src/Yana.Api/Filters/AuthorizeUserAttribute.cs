using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Yana.Application.Contracts.UserService;

namespace Yana.Api.Filters;

public sealed class AuthorizeUserAttribute : AuthorizeAttribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var httpContext = context.HttpContext;

        if (!httpContext.User.Identity?.IsAuthenticated ?? true)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var userRepositoryService = context.HttpContext.RequestServices.GetService<IUserRepositoryService>();
        var userAuthService = context.HttpContext.RequestServices.GetService<IUserAuthService>();

        var principal = httpContext.User;
        var userDto = userAuthService!.GetUserFromPrincipal(principal);

        if (userDto.ExternalId == Guid.Empty.ToString() || string.IsNullOrEmpty(userDto.Email))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var user = await userRepositoryService!.GetUserByExternalId(userDto.ExternalId);
        if (user is null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (context.Controller is YanaControllerBase controller)
        {
            controller.AuthenticatedUser = user;
        }

        await next();
    }
}