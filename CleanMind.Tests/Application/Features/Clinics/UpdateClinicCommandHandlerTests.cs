using System;
using System.Threading.Tasks;
using CleanMind.Application.Contracts.Persistence;
using CleanMind.Application.Contracts.Repositories;
using CleanMind.Application.Exceptions;
using CleanMind.Application.Features.Clinics.Commands.UpdateClinic;
using CleanMind.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;

namespace CleanMind.Tests.Application.Features.Clinics;

[TestClass]
public class UpdateClinicCommandHandlerTests
    {
    private IClinicRepository _clinicRepository;
    private IUnitOfWork _unitOfWork;
    private UpdateClinicCommandHandler _handler;


    [TestInitialize]
    public void Setup ()
        {
        _clinicRepository = Substitute.For<IClinicRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new UpdateClinicCommandHandler(_clinicRepository, _unitOfWork);
        }

    //happy path for calling the 'NotFoundException'
    [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Handle_When_Clinic_doesnt_Exist_Throws()
        {
            var command = new UpdateClinicCommand()
            {
                Id = Guid.CreateVersion7(),
                Name = "New Edited NamE"
                };
            _clinicRepository.GetById(command.Id).ReturnsNull();
            await _handler.HandleAsync(command);
        }


         //Happy path for a correct update
        [TestMethod]
        public async Task Handle_When_Clinic_Exists_Entity_Updated_And_Persisted()
        {
            var clinic = new Clinic("psychotherapy clinic test 01");
            var id = clinic.Id;
            var command = new UpdateClinicCommand()
            {
                Id = id,
                Name = "New Edited NamE",
            };

            _clinicRepository.GetById(id).Returns(clinic);
            
            await _handler.HandleAsync(command);

            await _clinicRepository.Received(1).Update(clinic);

            await _unitOfWork.Received(1).CommitAsync();

            Assert.AreEqual(clinic.Name,command.Name);
        }

        [TestMethod]
        public async Task Handle_When_There_Is_An_Exception_Update_Will_Rollback()
        {
            var clinic = new Clinic("psychotherapy clinic test 02");
            var id = clinic.Id;
            var command = new UpdateClinicCommand()
            {
                Id = id,
                Name = "New Name 02 for testing",
            };

            _clinicRepository.GetById(id).Returns(clinic);
            _clinicRepository.Update(clinic).Throws(new InvalidOperationException("Test Exception"));

            await Assert.ThrowsExceptionAsync<InvalidOperationException>(()=> _handler.HandleAsync(command));

            await _unitOfWork.Received(1).RollbackAsync();
        }


    }