using ClinicManager.Api.Models.PatientsModels;
using ClinicManager.Application.Commands.CommandsPatients.DeletePatients;
using ClinicManager.Application.Commands.CommandsPatients.InsertPatients;
using ClinicManager.Application.Queries.QueriesPatients;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Numerics;

namespace ClinicManager.Api.Controllers
{
    [ApiController]
    [Route("api/patients")]
    
    public class PatientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatientsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // GET api/patients
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string query = "")
        {
            var patientsQuery = new GetAllPatientsQuery(query);
            var result = await _mediator.Send(patientsQuery);

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
        
        // GETBYID api/patients
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetPatientsByIdQuery(id);
            var result = await _mediator.Send(query);

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        // POST api/patients
        [HttpPost]
        public async Task<IActionResult> PostPatients([FromBody] CreatePatientsInputModel model) 
        {
            var command = new InsertPatientCommand(
                model.Name,
                model.LastName,
                model.DateOfBirth,
                model.Phone,
                model.Email,
                model.CPF,
                model.BloodType,
                model.Height,
                model.Weight,
                model.Address
            );

            var result = await _mediator.Send(command);

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, model);
        }


        // PUT api/patients
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatients(int id, [FromBody] UpdatePatientsInputModel model)
        {
            var command = new InsertPatientCommand(
                model.Name,
                model.LastName,
                model.DateOfBirth,
                model.Phone,
                model.Email,
                model.CPF,
                model.BloodType,
                model.Height,
                model.Weight,
                model.Address
            );

            var result = await _mediator.Send(command);

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        // DELETE api/patients
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var command = new DeletePatientCommand(id);
            var result = await _mediator.Send(command);

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }
    }
}
