namespace Yana.Application.Features.Document.Command.CreateDocument;

public sealed class CreateDocumentCommand(UserProfile user, DocumentDto dto) : Command<CommandResponse<DocumentVm>>
{
    public UserProfile User { get; set; } = user;
    public DocumentDto Dto { get; set; } = dto;
}