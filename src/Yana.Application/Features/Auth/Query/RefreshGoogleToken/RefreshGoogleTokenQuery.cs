namespace Yana.Application.Features.Auth.Query.RefreshGoogleToken;

public class RefreshGoogleTokenQuery(string userId) : Request<Response<AccessTokenVm>>
{
    public string UserId { get; set; } = userId;
}