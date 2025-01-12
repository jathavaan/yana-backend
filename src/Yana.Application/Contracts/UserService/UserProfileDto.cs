namespace Yana.Application.Contracts.UserService;

public record UserProfileDto(
    string Email,
    string FirstName,
    string LastName,
    string ExternalId,
    AuthProvider AuthProvider);