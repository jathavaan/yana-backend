using Google.Apis.Auth.OAuth2.Responses;
using Microsoft.IdentityModel.Tokens;
using GoogleAuthorizationCodeFlow = Google.Apis.Auth.OAuth2.Flows.GoogleAuthorizationCodeFlow;

namespace Yana.Infrastructure.Services.AuthenticationService;

public class GoogleAuthenticationService : IAuthenticationService
{
    private readonly IEncryptionService _encryptionService;
    private readonly IOptionsMonitor<GoogleAuthenticationOptions> _googleAuthenticationOptions;
    private readonly GoogleAuthorizationCodeFlow _authorizationCodeFlow;
    private readonly ClientSecrets _clientSecrets;

    public GoogleAuthenticationService(IOptionsMonitor<GoogleAuthenticationOptions> googleAuthenticationOptions,
        IEncryptionService encryptionService)
    {
        _googleAuthenticationOptions = googleAuthenticationOptions;
        _encryptionService = encryptionService;

        _clientSecrets = new ClientSecrets
        {
            ClientId = _googleAuthenticationOptions.CurrentValue.ClientId,
            ClientSecret = _googleAuthenticationOptions.CurrentValue.ClientSecret
        };

        _authorizationCodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
        {
            ClientSecrets = _clientSecrets
        });
    }

    public async Task<(string idToken, string refreshToken)> GetUserTokens(string authoirzationCode)
    {
        var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
        {
            ClientSecrets = _clientSecrets,
            Scopes = _googleAuthenticationOptions.CurrentValue.Scopes
        });

        var tokenResponse = await flow.ExchangeCodeForTokenAsync(
            userId: Guid.CreateVersion7().ToString(),
            code: authoirzationCode,
            redirectUri: _googleAuthenticationOptions.CurrentValue.RedirectUri,
            taskCancellationToken: CancellationToken.None
        );

        var refreshToken = tokenResponse.RefreshToken;
        var idToken = tokenResponse.IdToken;

        return (idToken, refreshToken);
    }

    public async Task<string?> GetRefreshToken(string userId, string idToken)
    {
        var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
        {
            ClientSecrets = _clientSecrets
        });

        // flow.ExchangeCodeForTokenAsync(userId: userId, code: code)

        var credentials = await GoogleWebAuthorizationBroker.AuthorizeAsync(
            _clientSecrets,
            _googleAuthenticationOptions.CurrentValue.Scopes,
            userId,
            CancellationToken.None
        );


        Console.WriteLine(credentials.Token.RefreshToken);
        var encryptedRefreshToken = _encryptionService.Encrypt(credentials.Token.RefreshToken);
        return encryptedRefreshToken;
    }

    public async Task<string?> RefreshAccessToken(string externalId, string refreshToken)
    {
        try
        {
            var decryptedRefreshToken = _encryptionService.Decrypt(refreshToken);
            if (refreshToken.IsNullOrEmpty()) return null;

            var refreshTokenResponse = await _authorizationCodeFlow.RefreshTokenAsync(
                externalId,
                decryptedRefreshToken,
                CancellationToken.None
            );

            return refreshTokenResponse.IdToken;
        }
        catch (TokenResponseException)
        {
            return null;
        }
    }
}