using CleanMind.API.DTOS.Clinics;
using CleanMind.Application.Features.Clinics.Commands.CreateClinic;
using CleanMind.Application.Features.Clinics.Commands.DeleteClinic;
using CleanMind.Application.Features.Clinics.Commands.UpdateClinic;
using CleanMind.Application.Features.Clinics.Queries.GetClinicDetails;
using CleanMind.Application.Features.Clinics.Queries.GetClinicsLIst;
using CleanMind.Application.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CleanMind.API.Controllers
    {
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ClinicController : ControllerBase
        {
        private readonly IMediator _mediator;

        public ClinicController ( IMediator mediator )
            {
            _mediator = mediator;
            }

        /// <summary>
        /// List of All registered Clinics in the system
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<ClinicsListDto>>> GetAllClinics ()
            {
            var query = new GetClinicsListQuery();
            var result = await _mediator.SendAsync(query);
            return result;
            }


        /// <summary>
        ///  Find Clinic by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ClinicDetailsDto>> GetClinicById ( Guid id )
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
        public async Task<IActionResult> CreateClinic ( CreateClinicDto createClinicDto )
            {
            var command = new CreateClinicCommand() { Name = createClinicDto.Name };
            await _mediator.SendAsync(command);
            return Created();
            }


        /// <summary>
        /// Update the name of the clinic
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateClinicDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClinicName ( Guid id, UpdateClinicDto updateClinicDto )
        {
            var command = new UpdateClinicCommand()
            {
                Id = id,
                Name = updateClinicDto.Name,
            };

            //var results = _mediator.SendAsync(command);
            //await results;
            await _mediator.SendAsync(command);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClinic(Guid id)
        {
            var command = new DeleteClinicCommand()
            {
                Id = id,
            };

            await _mediator.SendAsync(command);

            return NoContent();
        }
        }
    }
