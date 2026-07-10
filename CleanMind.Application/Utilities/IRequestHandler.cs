namespace CleanMind.Application.Utilities;

public interface IRequestHandler<TRequest, TResponse>
    {
    Task<TResponse> HandleAsync ( TRequest request );
    }

public interface IRequestHandler<TRequest>
    {
    Task HandleAsync ( TRequest request );
    }