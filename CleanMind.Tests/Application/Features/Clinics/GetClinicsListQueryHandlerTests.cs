using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanMind.Application.Contracts.Repositories;
using CleanMind.Application.Features.Clinics.Queries.GetClinicsLIst;
using CleanMind.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace CleanMind.Tests.Application.Features.Clinics;

[TestClass]
public class GetClinicsListQueryHandlerTests
    {
    private IClinicRepository _clinicRepository;
    private GetClinicsListQueryHandler _handler;

    [TestInitialize]
    public void Setup ()
        {
        _clinicRepository = Substitute.For<IClinicRepository>();
        _handler = new GetClinicsListQueryHandler(_clinicRepository);
        }



    //show all clinics in the DB
    [TestMethod]
    public async Task Handle_Whne_There_Are_Clinics_Returns_List_Of_Them ()
        {
        var clinicsList = new List<Clinic>
        {
            new Clinic("Psychotherapy Clinic 01"),
            new Clinic("Psychotherapy Clinic 02"),
            new Clinic("Psychotherapy Clinic 03"),
            new Clinic("Psychotherapy Clinic 04"),
        };

        _clinicRepository.GetAll().Returns(clinicsList);

        var expected = clinicsList.Select(c => new ClinicsListDto()
            {
            Id = c.Id,
            Name = c.Name,
            }).ToList();


        var result = await _handler.HandleAsync(new GetClinicsListQuery());

        Assert.AreEqual(expected.Count, result.Count);

        for (int i = 0; i < expected.Count; ++i)
            {
            Assert.AreEqual(expected[i].Id, result[i].Id);
            Assert.AreEqual(expected[i].Name, result[i].Name);
            }
        }


    //show empty list when there is no clinic registered
    [TestMethod]
    public async Task Handle_When_There_Is_No_Clinic_Returns_Empty_Lsit ()
        {
        _clinicRepository.GetAll().Returns(new List<Clinic>());

        var result = await _handler.HandleAsync(new GetClinicsListQuery());

        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count);
        }

    }