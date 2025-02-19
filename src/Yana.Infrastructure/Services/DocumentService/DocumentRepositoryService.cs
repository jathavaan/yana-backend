using Yana.Application.Contracts.DocumentService;

namespace Yana.Infrastructure.Services.DocumentService;

public class DocumentRepositoryService : IDocumentRepositoryService
{
    private readonly YanaDbContext _dbContext;

    public DocumentRepositoryService(YanaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> HasUserDocumentPermission(UserProfile user, string documentId, DocumentRole minimumRole)
        => await _dbContext.DocumentHasUser.AnyAsync(x =>
            x.UserId == user.Id &&
            x.DocumentId == documentId &&
            x.Role >= minimumRole);
}