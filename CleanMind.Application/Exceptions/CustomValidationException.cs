using FluentValidation.Results;

namespace CleanMind.Application.Exceptions;

public class CustomValidationException : Exception
{
    public List<string> ValidationErrors { get; set; } = [];

    public CustomValidationException ( ValidationResult validationResult )
    {
        foreach (var validationError in validationResult.Errors)
        {
            ValidationErrors.Add($"Error Code: '{validationError.ErrorCode}', Error Message: '{validationError.ErrorMessage}'");
        }
    }
}