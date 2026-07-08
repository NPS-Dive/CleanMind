using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CleanMind.Application.Contracts.Repositories;
using CleanMind.Application.Exceptions;
using CleanMind.Application.Features.Clinics.Queries.GetClinicDetails;
using CleanMind.Domain.Entities;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace CleanMind.Tests.Application.Features.Clinics;

[TestClass]
public class GetClinicDetailsQueryHandlerTests
    {
    private IClinicRepository _clinicRepository;
    private GetClinicDetailsQueryHandler _handler;

    [TestInitialize]
    public void Setup ()
        {
        _clinicRepository = Substitute.For<IClinicRepository>();
        _handler = new GetClinicDetailsQueryHandler(_clinicRepository);
        }


        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Handle_Clinic_Not_Exists_Throws()
        {
            var id = Guid.CreateVersion7();
            var query = new GetClinicDetailsQuery(){Id = id};

            _clinicRepository.GetById(id).ReturnsNull();

            await _handler.HandleAsync(query);
        }


    [TestMethod]
    public async Task Handle_clinic_Exists_Returns_It ()
        {
        var clinic = new Clinic("Test Clinic 01");
        var id = clinic.Id;
        var query = new GetClinicDetailsQuery() { Id = id };

        _clinicRepository.GetById(id).Returns(clinic);

        var result = await _handler.HandleAsync(query);

        Assert.IsNotNull(result);
        Assert.AreEqual(id, result.Id);
        Assert.AreEqual("Test Clinic 01",result.Name);
        }
    }