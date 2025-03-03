namespace Yana.Application.Contracts.DocumentService;

public interface IDocumentRepositoryService
{
    public Task<bool> HasUserDocumentPermission(UserProfile user, string documentId, DocumentRole minimumRole);
    public Task<Document?> GetDocument(string documentId);
    public Task<Document> CreateDocument(UserProfile user, DocumentDto dto);
}