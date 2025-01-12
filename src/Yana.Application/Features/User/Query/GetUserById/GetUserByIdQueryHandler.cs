namespace Yana.Application.Features.User.Query.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Response<UserVm>>
{
    private readonly IUserRepositoryService _userRepositoryService;

    public GetUserByIdQueryHandler(IUserRepositoryService userRepositoryService)
    {
        _userRepositoryService = userRepositoryService;
    }

    public async Task<Response<UserVm>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepositoryService.GetUserById(request.UserId);
        return user is null
            ? new Response<UserVm>
            {
                ErrorCode = ErrorCode.NotFound,
                ErrorMessage = $"Could not find a user with ID {request.UserId}"
            }
            : new Response<UserVm>
            {
                Result = new UserVm
                {
                    UserId = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    CreatedDate = user.CreatedDate
                }
            };
    }
}