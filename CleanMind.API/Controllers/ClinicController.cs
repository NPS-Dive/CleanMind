using CleanMind.API.DTOS.Clinics;
using CleanMind.Application.Features.Clinics.Commands.CreateClinic;
using CleanMind.Application.Features.Clinics.Queries.GetClinicDetails;
using CleanMind.Application.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CleanMind.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ClinicController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClinicController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClinicDetailsDto>> GetClinicById(Guid id)
        {
            var query = new GetClinicDetailsQuery() { Id = id };
            var result = await _mediator.SendAsync(query);
            return result;
        }

        /// <summary>
        /// Create Clinic
        /// </summary>
        /// <param name="createClinicDto"></param>
        /// <returns>201 OK, if succeeded</returns>
        [HttpPost]
        public async Task<IActionResult> CreateClinic(CreateClinicDto createClinicDto)
        {
            var command = new CreateClinicCommand() { Name = createClinicDto.Name };
            await _mediator.SendAsync(command);
            return Created();
        }
    }
}
