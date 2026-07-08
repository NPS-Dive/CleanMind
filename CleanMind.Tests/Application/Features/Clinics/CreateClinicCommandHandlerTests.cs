using System;
using System.Threading.Tasks;
using CleanMind.Application.Contracts.Persistence;
using CleanMind.Application.Contracts.Repositories;
using CleanMind.Application.Features.Clinics.Commands.CreateClinic;
using CleanMind.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.ExceptionExtensions;


namespace CleanMind.Tests.Application.Features.Clinics;

[TestClass]
public class CreateClinicCommandHandlerTests
{
    private IClinicRepository _clinicRepository;
    private IUnitOfWork _unitOfWork;
    private CreateClinicCommandHandler _handler;

    [TestInitialize]
    public void Setup()
    {
        _clinicRepository = Substitute.For<IClinicRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler =new CreateClinicCommandHandler(_clinicRepository,_unitOfWork);
    }


    [TestMethod]
    public async Task Handle_Valid_Command_Returns_Clinic_Id()
    {
        var command = new CreateClinicCommand(){Name = "Clinic 01"};

        var clinic = new Clinic("Clinic 01");

        _clinicRepository.CreateAsync(Arg.Any<Clinic>()).Returns(clinic);

        var result = await _handler.HandleAsync(command);
        await _clinicRepository.Received(1).CreateAsync(Arg.Any<Clinic>());
        await _unitOfWork.Received(1).CommitAsync();
        Assert.AreEqual(clinic.Id,result);
    }

    [TestMethod]
    public async Task Handle_When_There_Is_An_Error_Rolls_Back()
    {
        var command = new CreateClinicCommand() { Name = "Clinic 01" };
        _clinicRepository.CreateAsync(Arg.Any<Clinic>()).ThrowsAsync<Exception>();

        await Assert.ThrowsExceptionAsync<Exception>(async () =>
        {
            await _handler.HandleAsync(command);
        });

        await _unitOfWork.Received(1).RollbackAsync();
    }
}