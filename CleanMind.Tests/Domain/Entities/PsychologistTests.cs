using System;
using CleanMind.Domain.Entities;
using CleanMind.Domain.Entities.Exceptions;
using CleanMind.Domain.Entities.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CleanMind.Tests.Domain.Entities;

[TestClass]
public class PsychologistTests
{
    [TestMethod]
    [ExpectedException(typeof(BusinessRuleException))]
    public void Constructor_Null_Name_Throws ()
    {
        var email = new Email("amir@mohammad.com");
        new Psychologist(null!, email);
    }

    [TestMethod]
    [ExpectedException(typeof(BusinessRuleException))]
    public void Constructor_Null_Email_Throws()
    {
        new Psychologist("amir", null!);
    }

    [TestMethod]
    public void Constructor_Valid_Psychologist_No_Exception ()
    {
        var email = new Email("amir@mohammad.com");
        new Psychologist("psychologist 01",email);
    }
    }