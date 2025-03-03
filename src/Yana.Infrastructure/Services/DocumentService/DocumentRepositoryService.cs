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

    public async Task<Document?> GetDocument(string documentId)
        => await _dbContext.Documents
            .Include(x => x.DocumentHasUsers)
            .FirstOrDefaultAsync(x => x.Id == documentId);

    public async Task<Document> CreateDocument(UserProfile user, DocumentDto dto)
    {
        var document = new Document
        {
            Title = dto.Title,
            Type = dto.Type,
            Tags = dto.Tags.Select(x => new Tag()
                {
                    Name = x.Name
                })
                .ToList(),
            DocumentHasUsers =
            [
                new DocumentHasUser
                {
                    Role = DocumentRole.Owner
                }
            ],
            Users = [user]
        };

        _dbContext.Documents.Add(document);
        await _dbContext.SaveChangesAsync();

        return document;
    }
}