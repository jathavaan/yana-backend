using Yana.Domain.Enums;

namespace Yana.Application.ViewModels;

public class UserVm
{
    public string? UserId { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
}