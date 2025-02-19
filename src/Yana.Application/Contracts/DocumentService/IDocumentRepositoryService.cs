namespace Yana.Application.Contracts.DocumentService;

public interface IDocumentRepositoryService
{
    public Task<bool> HasUserDocumentPermission(UserProfile user, string documentId, DocumentRole minimumRole);
}