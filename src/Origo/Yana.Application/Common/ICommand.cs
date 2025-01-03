namespace Origo.Application.Common;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
    string CorrelationId { get; }
}

public abstract class Command<TResponse> : ICommand<TResponse>
{
    public string CorrelationId { get; } = Guid.NewGuid().ToString();
}