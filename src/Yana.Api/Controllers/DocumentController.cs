using Yana.Application.Contracts.DocumentService;
using Yana.Application.Features.Document.Command.CreateDocument;

namespace Yana.Api.Controllers;

[AuthorizeUser]
public sealed class DocumentController(IMediator mediator) : YanaControllerBase(mediator)
{
    [HttpPost("{documentId}/tiles/{tileId}/save")]
    [Produces("application/json")]
    [ApiConventionMethod(typeof(SwaggerApiConvention), nameof(SwaggerApiConvention.StatusResponseTypes))]
    [ActionName(nameof(SaveTile))]
    public async Task<ActionResult<bool>> SaveTile(string documentId, string tileId, SaveTileDto dto)
        => await SendCommand<bool, SaveTileCommand>(new SaveTileCommand(documentId, tileId, dto));

    [HttpPost("create")]
    [Produces("application/json")]
    [ApiConventionMethod(typeof(SwaggerApiConvention), nameof(SwaggerApiConvention.StatusResponseTypes))]
    [ActionName(nameof(CreateDocument))]
    public async Task<ActionResult<DocumentVm>> CreateDocument(DocumentDto dto)
        => await SendCommand<DocumentVm, CreateDocumentCommand>(new CreateDocumentCommand(AuthenticatedUser!, dto));
}