namespace CleanMind.Domain.Entities.Exceptions;

public class BusinessRuleException : Exception
    {
    public BusinessRuleException ( string message )
        : base(message)
        {

        }
    }