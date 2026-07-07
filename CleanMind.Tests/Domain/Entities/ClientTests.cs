using System;
using CleanMind.Domain.Entities;
using CleanMind.Domain.Entities.Exceptions;
using CleanMind.Domain.Entities.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleanMind.Tests.Domain.Entities;

[TestClass]
public class ClientTests
{
    [TestMethod]
    [ExpectedException(typeof(BusinessRuleException))]
    public void Constructor_Null_Name_Throws ()
    {
        var email = new Email("amir@mohammad.com");
        new Client(null!,email);
    }

    [TestMethod]
    [ExpectedException(typeof(BusinessRuleException))]
    public void Constructor_Null_Email_Throws ()
    {
        new Client("amir", null!);
    }

    [TestMethod]
    public void Constructor_Valid_Client_No_Exception ()
    {
        var email = new Email("amir@mohammad.com");
        new Client("Client 01", email);
    }
    }