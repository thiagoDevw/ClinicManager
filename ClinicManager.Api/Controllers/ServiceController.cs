using ClinicManager.Api.Models.ServiceModels;
using ClinicManager.Application.Models.ServiceModels;
using ClinicManager.Application.Services.ServicesService;
using ClinicManager.Core.Entities;
using ClinicManager.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IService _service;
        public ServiceController(IService service)
        {
            _service = service;
        }

        // GET api/service
        [HttpGet]
        public IActionResult GetAll([FromQuery] string? query = "")
        {
            var result = _service.GetAll(query);

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        // GETBYID api/service
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetById(id);

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }


        // POST api/service
        [HttpPost]
        public IActionResult PostService([FromBody] CreateServiceInputModel model)
        {
            var result = _service.Insert(model);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, model);
        }

        // PUT api/service
        [HttpPut("{id}")]
        public IActionResult PutService(int id, [FromBody] UpdateServiceInputModel model)
        {
            if (model == null)
            {
                return BadRequest("Os dados do serviço são obrigatórios.");
            }

            var result = _service.Update(id, model);

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        // DELETE api/service
        [HttpDelete("{id}")]
        public IActionResult DeleteService(int id)
        {
            var result = _service.DeleteById(id);

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }
    }
}
