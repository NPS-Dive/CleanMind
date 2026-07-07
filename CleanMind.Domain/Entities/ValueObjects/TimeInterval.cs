using CleanMind.Domain.Entities.Exceptions;

namespace CleanMind.Domain.Entities.ValueObjects;

public record TimeInterval
    {
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    public TimeInterval ( DateTime start, DateTime end )
        {
        if (start > end)
            {
            throw new BusinessRuleException($"the start time  cannot be after end time !");
            }
      
        Start = start;
        End = end;
        }
    }