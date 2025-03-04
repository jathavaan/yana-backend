namespace Yana.Application.Contracts.DocumentService;

public sealed record DocumentDto(DocumentType Type, string Title, ICollection<TagDto> Tags);

public sealed record TagDto(int Id);