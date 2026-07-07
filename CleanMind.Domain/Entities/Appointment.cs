using CleanMind.Domain.Entities.Enums;
using CleanMind.Domain.Entities.Exceptions;
using CleanMind.Domain.Entities.ValueObjects;

namespace CleanMind.Domain.Entities;

public class Appointment
    {
    public Guid Id { get; private set; }
    public Guid? ClientId { get; private set; }
    public Guid? PsychologistId { get; private set; }
    public Guid? PsychiatristId { get; private set; }
    public Guid? ClinicId { get; private set; }
    public AppointmentStatus Status { get; private set; }
    public TimeInterval TimeInterval { get; private set; }
    public Client? Client { get; private set; }
    public Clinic? Clinic { get; private set; }

    public Appointment ( Guid clientId, Guid psychologistId, Guid psychiatristId, Guid clinicId,
        TimeInterval timeInterval, Client? client, Clinic? clinic )

        {
        if (timeInterval.Start < DateTime.UtcNow)
            {
            throw new BusinessRuleException($"the start time  cannot be in the past!");
            }

        Id = Guid.CreateVersion7();
        ClientId = clientId;
        PsychologistId = psychologistId;
        PsychiatristId = psychiatristId;
        ClinicId = clinicId;
        Status = AppointmentStatus.Scheduled;
        TimeInterval = timeInterval;
        }

    public void Cancel ()
        {
        if (Status != AppointmentStatus.Scheduled)
            {
            throw new BusinessRuleException("Only 'Scheduled' appointments can be cancelled!");
            }

        Status = AppointmentStatus.Cancelled;
        }

    public void Completed ()
        {
        if (Status != AppointmentStatus.Scheduled)
            {
            throw new BusinessRuleException("Only 'Scheduled' appointments can be completed");
            }

        Status = AppointmentStatus.Done;
        }
    }