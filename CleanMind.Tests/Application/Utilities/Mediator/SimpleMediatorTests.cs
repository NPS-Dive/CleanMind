using System;
using System.Threading.Tasks;
using CleanMind.Application.Exceptions;
using CleanMind.Application.Utilities;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace CleanMind.Tests.Application.Utilities.Mediator;

[TestClass]
public class SimpleMediatorTests
{
    public class FalseRequest : IRequest<string>
    {
        public required string Name { get; set; }
    }

    public class FalseRequestValidator : AbstractValidator<FalseRequest>
    {
        public FalseRequestValidator ()
        {
            RuleFor(static x => x.Name).NotEmpty();
        }
    }

    [TestMethod]
    [ExpectedException(typeof(MediatorException))]
    public async Task Send_Without_Registered_Handler_Throws ()
    {
        var request = new FalseRequest() { Name = "Amir Mohammad" };
        var serviceProvider = Substitute.For<IServiceProvider>();

        serviceProvider
            .GetService(typeof(IRequestHandler<FalseRequest, string>))
            .ReturnsNull();

        var mediator = new SimpleMediator(serviceProvider);

        var result = await mediator.SendAsync(request);
    }

    [TestMethod]
    [ExpectedException(typeof(CustomValidationException))]
    public async Task Send_Invalid_Command_Throws()
    {
        var request = new FalseRequest() { Name = "" };

        var serviceProvider = Substitute.For<IServiceProvider>();
        var validator = new FalseRequestValidator();

        serviceProvider
            .GetService(typeof(IValidator<FalseRequest>))
            .Returns(validator);

        var mediator = new SimpleMediator(serviceProvider);

        var result = mediator.SendAsync(request);
        await result;
    }

    [TestMethod]
    public async Task Send_With_Registered_Handler_Handle_Is_Executed ()
    {
        var request = new FalseRequest() { Name = "Amir Mohammad" };
        var handlerMock = Substitute.For<IRequestHandler<FalseRequest, string>>();

        var serviceProvider = Substitute.For<IServiceProvider>();

        serviceProvider
            .GetService(typeof(IRequestHandler<FalseRequest, string>))
            .Returns(handlerMock);

        var mediator = new SimpleMediator(serviceProvider);

        var result = await mediator.SendAsync(request);

        await handlerMock.Received(1).HandleAsync(request);
    }
}