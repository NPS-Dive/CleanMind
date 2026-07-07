using System;
using CleanMind.Domain.Entities;
using CleanMind.Domain.Entities.Exceptions;
using CleanMind.Domain.Entities.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CleanMind.Tests.Domain.Entities;

[TestClass]
public class ClinicTests
{
    [TestMethod]
    [ExpectedException(typeof(BusinessRuleException))]
    public void Constructor_Null_Name_Throws()
    {
        new Clinic(null!);
    }

    [TestMethod]
    public void Constructor_Valid_Clinic_No_Exception()
    {
        new Clinic("Clinic 01");
    }
}