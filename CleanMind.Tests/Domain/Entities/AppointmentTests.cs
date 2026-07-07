using System;
using CleanMind.Domain.Entities;
using CleanMind.Domain.Entities.Enums;
using CleanMind.Domain.Entities.Exceptions;
using CleanMind.Domain.Entities.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleanMind.Tests.Domain.Entities;

[TestClass]
public class AppointmentTests
    {
    private Guid _clientId = Guid.CreateVersion7();
    private Guid _psychologistId = Guid.CreateVersion7();
    private Guid _psychiatristId = Guid.CreateVersion7();
    private Guid _clinicId = Guid.CreateVersion7();
    private TimeInterval _interval = new TimeInterval(DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2));

    //make appointment in which start time is before 'DateTime.UtcNow'
    [TestMethod]
    [ExpectedException(typeof(BusinessRuleException))]
    public void Constructor_Start_Time_Before_Now_Throws ()
        {
        var interval = new TimeInterval(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(1));
        var appointment = new Appointment(_clientId, _psychologistId, _psychiatristId, _clinicId, interval, null, null);
        }


    //cancel the appointment which is cancelled before
    [TestMethod]
    [ExpectedException(typeof(BusinessRuleException))]
    public void Constructor_Cancel_Not_Scheduled_Appointment_Throws ()
        {
        var appointment = new Appointment(_clientId, _psychologistId, _psychiatristId, _clinicId, _interval, null, null);
        appointment.Cancel();
        appointment.Cancel();
        }

    //complete the appointment which is cancelled before
    [TestMethod]
    [ExpectedException(typeof(BusinessRuleException))]
    public void Constructor_Complete_canceled_Appointment_Throws ()
        {
        var appointment = new Appointment(_clientId, _psychologistId, _psychiatristId, _clinicId, _interval, null, null);
        appointment.Cancel();
        appointment.Completed();
        }

     // make a valid appointment
    [TestMethod]
    public void Constructor_Valid_Appointment_Status_Scheduled ()
        {
        var appointment = new Appointment(_clientId, _psychologistId, _psychiatristId, _clinicId, _interval, null, null);
        Assert.AreEqual(_clientId, appointment.ClientId);
        Assert.AreEqual(_psychologistId, appointment.PsychologistId);
        Assert.AreEqual(_psychiatristId, appointment.PsychiatristId);
        Assert.AreEqual(_clinicId, appointment.ClinicId);
        Assert.AreEqual(_interval, appointment.TimeInterval);
        Assert.AreEqual(AppointmentStatus.Scheduled, appointment.Status);
        Assert.AreNotEqual(Guid.Empty, appointment.Id);
        }

    //status change to cancel for a valid appointment
    [TestMethod]
    public void Constructor_Cancel_Appointment_changes_Status_To_Canceled ()
        {
        var appointment = new Appointment(_clientId, _psychologistId, _psychiatristId, _clinicId, _interval, null, null);
        appointment.Cancel();
        Assert.AreEqual(AppointmentStatus.Cancelled, appointment.Status);
        }

    //status change to complete for a valid appointment
    [TestMethod]
    public void Constructor_Complete_Appointment_changes_Status_To_Completed ()
        {
        var appointment = new Appointment(_clientId, _psychologistId, _psychiatristId, _clinicId, _interval, null, null);
        appointment.Completed();
        Assert.AreEqual(AppointmentStatus.Done, appointment.Status);
        }

    }