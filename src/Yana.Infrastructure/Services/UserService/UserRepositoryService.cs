using Microsoft.IdentityModel.Tokens;
using Yana.Domain.Enums;

namespace Yana.Infrastructure.Services.UserService;

public sealed class UserRepositoryService : IUserRepositoryService
{
    private readonly YanaDbContext _dbContext;
    private readonly IAuthenticationService _authenticationService;

    public UserRepositoryService(YanaDbContext dbContext, IEncryptionService encryptionService,
        IAuthenticationService authenticationService)
    {
        _dbContext = dbContext;
        _authenticationService = authenticationService;
    }

    public async Task<UserProfile?> GetUserById(string userId)
        => await _dbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == userId);

    public async Task<UserProfile?> GetUserByEmail(string email)
        => await _dbContext.UserProfiles
            .Include(x => x.ExternalUserProfiles)
            .FirstOrDefaultAsync(x => x.Email == email);


    public async Task<UserProfile?> GetUserByExternalId(string externalUserId)
        => await _dbContext.UserProfiles
            .Include(x => x.ExternalUserProfiles)
            .FirstOrDefaultAsync(x => x.ExternalUserProfiles.Any(y => y.Id == externalUserId));

    public async Task<string?> GetExternalIdById(string userId, AuthProvider authProvider)
        => await _dbContext.UserProfiles
            .Include(x => x.ExternalUserProfiles)
            .Where(x => x.Id == userId)
            .SelectMany(x => x.ExternalUserProfiles)
            .Where(x => x.AuthProvider == authProvider)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();


    public async Task<ExternalUserProfile?> GetExternalUserProfile(string userId, AuthProvider authProvider)
        => await _dbContext.ExternalUserProfiles
            .Include(x => x.User)
            .Where(x => x.User.Id == userId && x.AuthProvider == authProvider)
            .FirstOrDefaultAsync();

    public async Task<UserProfile> UpsertUser(UserProfileDto userDto)
    {
        var user = await GetUserByEmail(userDto.Email);

        switch (user)
        {
            case not null when user.ExternalUserProfiles.Any(x => x.Id == userDto.ExternalId):
                return user;
            case not null:
                user.ExternalUserProfiles =
                [
                    new ExternalUserProfile
                    {
                        Id = userDto.ExternalId,
                        AuthProvider = userDto.AuthProvider,
                    }
                ];

                _dbContext.Update(user);
                break;
            case null:
                user = new UserProfile
                {
                    Email = userDto.Email,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    ExternalUserProfiles =
                    [
                        new ExternalUserProfile
                        {
                            Id = userDto.ExternalId,
                            AuthProvider = userDto.AuthProvider,
                        }
                    ]
                };

                _dbContext.Add(user);
                break;
        }

        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<bool> UpsertRefreshToken(UserProfile user, AuthProvider authProvider)
    {
        var externalUserProfile = user.ExternalUserProfiles.FirstOrDefault(x => x.AuthProvider == authProvider);
        var authProviderUserId = externalUserProfile?.Id;
        if (externalUserProfile is null || authProviderUserId is null) return false;

        var refreshToken = await _authenticationService.GetRefreshToken(authProviderUserId!, "");
        if (refreshToken.IsNullOrEmpty()) return false;

        externalUserProfile.RefreshToken = refreshToken;
        _dbContext.Update(externalUserProfile);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}