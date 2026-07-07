using System;
using CleanMind.Domain.Entities.Exceptions;
using CleanMind.Domain.Entities.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleanMind.Tests.Domain.ValueObjects;

[TestClass]
public class TimeIntervalTests
{
    [TestMethod]
    [ExpectedException(typeof(BusinessRuleException))]
    public void Constructor_Start_Is_After_End_Throws()
    {
        new TimeInterval(DateTime.UtcNow, DateTime.UtcNow.AddDays(-1));
    }

    [TestMethod]
    public void Constructor_Valid_Time_Interval_No_Exception()
    {
        new TimeInterval(DateTime.UtcNow,DateTime.UtcNow.AddHours(1));
    }
}