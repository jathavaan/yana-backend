using Google.Apis.Auth.OAuth2.Responses;
using GoogleAuthorizationCodeFlow = Google.Apis.Auth.OAuth2.Flows.GoogleAuthorizationCodeFlow;

namespace Yana.Infrastructure.Services.AuthenticationService;

public class GoogleAuthenticationService : IAuthenticationService
{
    private readonly IOptionsMonitor<GoogleAuthenticationOptions> _googleAuthenticationOptions;
    private readonly GoogleAuthorizationCodeFlow _authorizationCodeFlow;
    private readonly IUserRepositoryService _userRepositoryService;

    public GoogleAuthenticationService(IOptionsMonitor<GoogleAuthenticationOptions> googleAuthenticationOptions,
        IUserRepositoryService userRepositoryService)
    {
        _googleAuthenticationOptions = googleAuthenticationOptions;
        _userRepositoryService = userRepositoryService;
        _authorizationCodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
        {
            ClientSecrets = new ClientSecrets
            {
                ClientId = _googleAuthenticationOptions.CurrentValue.ClientId,
                ClientSecret = _googleAuthenticationOptions.CurrentValue.ClientSecret
            }
        });
    }

    public async Task<string?> RefreshAccessToken(string userId, string refreshToken)
    {
        var googleUserId = await _userRepositoryService.GetExternalIdById(userId, AuthProvider.Google);
        try
        {
            var refreshTokenResponse =
                await _authorizationCodeFlow.RefreshTokenAsync(googleUserId, "asdfasdfasdf", CancellationToken.None);

            return refreshTokenResponse.IdToken;
        }
        catch (TokenResponseException)
        {
            return null;
        }
    }
}