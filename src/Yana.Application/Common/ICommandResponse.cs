namespace Yana.Application.Common;

// TODO: Implement event bus to publish events
public interface ICommandResponse
{
}

public class CommandResponse<T> : Response<T>, ICommandResponse
{
}