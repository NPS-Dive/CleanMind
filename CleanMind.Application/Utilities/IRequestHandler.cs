namespace CleanMind.Application.Utilities;

public interface IRequestHandler<TRequest, TResponse>
    {
    Task<TResponse> HandleAsync ( TRequest request );
    }