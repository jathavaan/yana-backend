namespace Yana.Application.Features.User.Query.GetUserById;

public class GetUserByIdQuery(string userId) : Request<Response<UserVm>>
{
    public string UserId { get; } = userId;
}