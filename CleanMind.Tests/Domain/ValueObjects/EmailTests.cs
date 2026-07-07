using System;
using CleanMind.Domain.Entities.Exceptions;
using CleanMind.Domain.Entities.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleanMind.Tests.Domain.ValueObjects;

[TestClass]
public class EmailTests
{
    [TestMethod]
    [ExpectedException(typeof(BusinessRuleException))]
    public void Constructor_NullEmail_Throws()
    {
        new Email(null!);
        //Assert.ThrowsException<BusinessRuleException>(() => new Email(null!));
        }

    [TestMethod]
    [ExpectedException(typeof(BusinessRuleException))]
    public void Constructore_Email_Without_At_Throws()
    {
        new Email("amir.rimapour.com");
    }

    [TestMethod]
    public void Constructor_Valid_Email_No_Exceptions()
    {
        new Email("amir@mohammad.com");
    }
}