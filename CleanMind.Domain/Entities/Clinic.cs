using CleanMind.Domain.Entities.Exceptions;

namespace CleanMind.Domain.Entities;

public class Clinic ( Guid id, string? name = null )
{
    public Guid Id { get; private set; } = id;
    public string? Name { get; private set; } = name;

    public Clinic ( string name ) : this(Guid.CreateVersion7(), name)
    {
        EnforceNameBusinessRules(name);
    }

    public void UpdateName ( string name )

    {
        EnforceNameBusinessRules(name);
        Name = name;
    }


    private static void EnforceNameBusinessRules ( string name )
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new BusinessRuleException($"The {nameof(name)} is mandatory");
        }
    }
}