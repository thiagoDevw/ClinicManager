using ClinicManager.Api.Models.PatientsModels;
using ClinicManager.Application.Models.PatientsModels;
using ClinicManager.Application.Services.ServicesPatient;
using ClinicManager.Core.Entities;
using ClinicManager.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManager.Api.Controllers
{
    [ApiController]
    [Route("api/patients")]
    
    public class PatientsController : ControllerBase
    {
        private readonly ClinicDbContext _context;
        private readonly IPatientService _patientService;
        public PatientsController(ClinicDbContext context, IPatientService patientService)
        {
            _context = context;
            _patientService = patientService;
        }

        // GET api/patients
        [HttpGet]
        public IActionResult GetAll([FromQuery] string query = "")
        {
            var result = _patientService.GetAll(query);

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
        
        // GETBYID api/patients
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _patientService.GetById(id);

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        // POST api/patients
        [HttpPost]
        public IActionResult PostPatients([FromBody] CreatePatientsInputModel model) 
        {
            var result = _patientService.Insert(model);


            return CreatedAtAction(nameof(GetById), new { id = result.Data }, model);
        }


        // PUT api/patients
        [HttpPut("{id}")]
        public IActionResult PutPatients(int id, [FromBody] UpdatePatientsInputModel model)
        {
            if (model == null)
            {
                return BadRequest("Os dados do paciente são obrigatórios.");
            }

            var result = _patientService.Update(id, model);

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        // DELETE api/patients
        [HttpDelete("{id}")]
        public IActionResult DeletePatient(int id)
        {
            var result = _patientService.DeleteById(id);

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }
    }
}
