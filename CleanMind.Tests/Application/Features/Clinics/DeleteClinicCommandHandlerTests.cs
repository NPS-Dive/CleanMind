using System;
using System.Threading.Tasks;
using CleanMind.Application.Contracts.Persistence;
using CleanMind.Application.Contracts.Repositories;
using CleanMind.Application.Exceptions;
using CleanMind.Application.Features.Clinics.Commands.DeleteClinic;
using CleanMind.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;

namespace CleanMind.Tests.Application.Features.Clinics;

[TestClass]
public class DeleteClinicCommandHandlerTests
{


    private IClinicRepository _clinicRepository;
    private IUnitOfWork _unitOfWork;
    private DeleteClinicCommandHandler _handler;

    [TestInitialize]
    public void Setup ()
    {
        _clinicRepository = Substitute.For<IClinicRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new DeleteClinicCommandHandler(_clinicRepository, _unitOfWork);
    }


    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task Handle_When_Clinic_Not_Exists_Throws()
    {
        var command = new DeleteClinicCommand()
        {
            Id = Guid.CreateVersion7(),
        };

        _clinicRepository.GetById(command.Id).ReturnsNull();
        await _handler.HandleAsync(command);
    }


    [TestMethod]
    public async Task Handle_When_Clinic_Exists_Delete_And_Commit_Are_Called()
    {
        var clinic = new Clinic("test clinic 01");
        var command = new DeleteClinicCommand()
        {
            Id = clinic.Id,
        };

        _clinicRepository.GetById(command.Id).Returns(clinic);
        await _handler.HandleAsync(command);

        await _clinicRepository.Received(1).Delete(clinic);
        await _unitOfWork.Received(1).CommitAsync();

    }

    [TestMethod]
    public async Task Handle_When_Exception_Occurs_while_Deleting_Rollback_Activated()
    {
        var clinic = new Clinic("test clinic 02");
        var command = new DeleteClinicCommand()
        {
            Id = clinic.Id,
        };

        _clinicRepository.GetById(command.Id).Returns(clinic);
        _clinicRepository.Delete(clinic).Throws(new InvalidOperationException("Exception Occurs!"));

        await Assert.ThrowsExceptionAsync<InvalidOperationException>(()=> _handler.HandleAsync(command));
        await _unitOfWork.Received(1).RollbackAsync();
    }

    }