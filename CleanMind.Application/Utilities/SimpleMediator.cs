using CleanMind.Application.Exceptions;
using FluentValidation;
using FluentValidation.Results;

namespace CleanMind.Application.Utilities;

public class SimpleMediator : IMediator
    {
    private readonly IServiceProvider _serviceProvider;

    public SimpleMediator ( IServiceProvider serviceProvider )
        {
        _serviceProvider = serviceProvider;
        }
    public async Task<TResponse> SendAsync<TResponse> ( IRequest<TResponse> request )
        {

        var validatorType = typeof(IValidator<>).MakeGenericType(request.GetType());

        var validator = _serviceProvider.GetService(validatorType);

        if (validator is not null)
        {
            var validateMethod = validatorType.GetMethod("ValidateAsync");

            var taskToValidate =
                (Task)validateMethod!.Invoke(validator, new object[] { request, CancellationToken.None })!;
            await taskToValidate;

            var innerResult = taskToValidate.GetType().GetProperty("Result");
            var validationResult = (ValidationResult)innerResult!.GetValue(taskToValidate);

            if (!validationResult.IsValid)
            {
                throw new CustomValidationException(validationResult);
            }
        }


        var handlerType = typeof(IRequestHandler<,>)
            .MakeGenericType(request.GetType(), typeof(TResponse));

        var handler = _serviceProvider.GetService(handlerType);

        if (handler is null)
        {
            throw new MediatorException($"Handler was not found for '{request.GetType().Name}'!");
        }

        var method = handlerType.GetMethod("HandleAsync");
        var result = (Task<TResponse>)method.Invoke(handler, new object[] { request })!;
        return await result;
        }
    }