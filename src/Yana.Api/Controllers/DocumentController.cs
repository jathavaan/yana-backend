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
}