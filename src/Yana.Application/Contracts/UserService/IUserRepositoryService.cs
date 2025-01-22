namespace Yana.Application.Contracts.UserService;

public interface IUserRepositoryService
{
    public Task<UserProfile?> GetUserById(string userId);
    public Task<UserProfile?> GetUserByEmail(string email);
    public Task<UserProfile?> GetUserByExternalId(string externalUserId);
    public Task<string?> GetExternalIdById(string userId, AuthProvider authProvider);
    public Task<ExternalUserProfile?> GetExternalUserProfile(string userId, AuthProvider authProvider);
    public Task<UserProfile> UpsertUser(UserProfileDto userDto);
    public Task<bool> UpsertRefreshToken(UserProfile user, AuthProvider authProvider);
}