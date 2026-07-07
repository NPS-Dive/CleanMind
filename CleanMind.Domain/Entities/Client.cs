using System;
using CleanMind.Domain.Entities.Exceptions;
using CleanMind.Domain.Entities.ValueObjects;


namespace CleanMind.Domain.Entities;

public class Client 
    {
    public Guid Id { get; private set; }
    public string Name { get; private set; } 
    public Email Email { get; private set; }

    public Client ( string name,Email email ) 
    {
        if (string.IsNullOrWhiteSpace(name))
            {
            throw new BusinessRuleException($"the {nameof(name)} is mandatory!");
            }

            if (email is null)
            {
                throw new BusinessRuleException($"the {nameof(email)} is required");
            }
        Name = name;
        Email = email;
        Id = Guid.CreateVersion7();
        }
    }