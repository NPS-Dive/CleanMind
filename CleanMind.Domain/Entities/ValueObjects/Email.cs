using System;
using System.Collections.Generic;
using System.Text;
using CleanMind.Domain.Entities.Exceptions;

namespace CleanMind.Domain.Entities.ValueObjects

    {
    public record Email
        {
        public string EmailValue { get; }

        public Email ( string email )
            {
            if (string.IsNullOrWhiteSpace(email))
                {
                throw new BusinessRuleException($"the {nameof(email)} is mandatory!");
                }

            if (!email.Contains("@"))
                {
                throw new BusinessRuleException($"the {email} is not in the correct format");
                }
            EmailValue = email.Trim().ToLowerInvariant();
            }

        }
    }
