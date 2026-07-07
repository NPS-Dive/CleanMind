using System;
using CleanMind.Domain.Entities;
using CleanMind.Domain.Entities.Exceptions;
using CleanMind.Domain.Entities.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleanMind.Tests.Domain.Entities;

[TestClass]
public class PsychiatristTests
{
    [TestMethod]
    [ExpectedException(typeof(BusinessRuleException))]
    public void Constructor_Null_Name_Throws ()
    {
        var email = new Email("amir@mohammad.com");
        new Psychiatrist(null!,email);
    }

    [TestMethod]
    [ExpectedException(typeof(BusinessRuleException))]
    public void Constructor_Null_Email_Throws ()
    {
        new Psychiatrist("amir", null!);
    }

    [TestMethod]
    public void Constructor_Valid_Psychiatrist_No_Exception ()
    {
        var email = new Email("amir@mohammad.com");
        new Psychiatrist("psychiatrist 01", email);
    }
    }