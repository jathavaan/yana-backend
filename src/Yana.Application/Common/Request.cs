namespace Yana.Application.Common;

public class Request<TResponse> : IRequest<TResponse> where TResponse : Response
{
}