using ClinicManager.Api.Models.DoctorModels;
using ClinicManager.Application.Models.DoctorModels;
using ClinicManager.Application.Services.ServicesDoctor;
using ClinicManager.Core.Entities;
using ClinicManager.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ClinicManager.Api.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }


        // GET api/doctors
        [HttpGet]
        public IActionResult GetAll([FromQuery] string query = "")
        {
            var result = _doctorService.GetAll(query);

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        // GETBYID api/doctors
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _doctorService.GetById(id);

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        // POST api/doctors
        [HttpPost]
        public IActionResult PostDoctor([FromBody] CreateDoctorInputModel model)
        {
            var result = _doctorService.Insert(model);


            return CreatedAtAction(nameof(GetById), new { id = result.Data }, model);
        }

        // PUT api/doctors
        [HttpPut("{id}")]
        public IActionResult PutDoctor(int id, [FromBody] UpdateDoctorInputModel model)
        {
            if (model == null)
            {
                return BadRequest("Os dados do médico são obrigatórios.");
            }

            var result = _doctorService.Update(id, model);

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        // DELETE api/doctors
        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(int id)
        {
            var result = _doctorService.DeleteById(id);

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }
    }
}
