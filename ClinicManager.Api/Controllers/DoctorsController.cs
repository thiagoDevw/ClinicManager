using ClinicManager.Api.Models.DoctorModels;
using ClinicManager.Application.Commands.CommandsDoctors.DeleteDoctor;
using ClinicManager.Application.Commands.CommandsDoctors.InsertDoctor;
using ClinicManager.Application.Commands.CommandsDoctors.UpdateDoctor;
using ClinicManager.Application.Queries.Doctors.GetAllDoctors;
using ClinicManager.Application.Queries.Doctors.GetByIdDoctor;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManager.Api.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DoctorsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // GET api/doctors
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string query = "")
        {
            // var result = _doctorService.GetAll(query);

            var doctorsQuery = new GetAllDoctorsQuery(query);
            var result = await _mediator.Send(doctorsQuery);

            return Ok(result);
        }

        // GETBYID api/doctors
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var doctorQuery = new GetDoctorByIdQuery(id);
            var result = await _mediator.Send(doctorQuery);

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        // POST api/doctors
        [HttpPost]
        public async Task<IActionResult> PostDoctor([FromBody] CreateDoctorInputModel model)
        {
            var doctor = new InsertDoctorCommand(
                model.Name,
                model.LastName,
                model.DateOfBirth,
                model.Phone,
                model.Email,
                model.CPF,
                model.BloodType,
                model.Address,
                model.Specialty,
                model.CRM
            );

            var result = await _mediator.Send(doctor);


            return CreatedAtAction(nameof(GetById), new { id = result.Data }, model);
        }

        // PUT api/doctors
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor(int id, [FromBody] UpdateDoctorInputModel model)
        {
            if (model == null)
            {
                return BadRequest("Os dados do médico são obrigatórios.");
            }

            var command = new UpdateDoctorCommand(
                model.Name,
                model.LastName,
                model.DateOfBirth,
                model.Phone,
                model.Email,
                model.CPF,
                model.BloodType,
                model.Address,
                model.Specialty,
                model.CRM
            );

            var result = await _mediator.Send(command);

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        // DELETE api/doctors
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var command = new DeleteDoctorCommand(id);

            var result = await _mediator.Send(command);

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }
    }
}
