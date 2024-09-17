using Azure.Core;
using ClinicManager.Api.Models.ServiceModels;
using ClinicManager.Application.Commands.CommandsServices.DeleteService;
using ClinicManager.Application.Commands.CommandsServices.InsertService;
using ClinicManager.Application.Commands.CommandsServices.UpdateService;
using ClinicManager.Application.Queries.QueriesServices;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Linq;

namespace ClinicManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/service
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? query = "")
        {
            var servicesQuery = new GetAllServicesQuery(query);
            var result = await _mediator.Send(servicesQuery);

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        // GETBYID api/service
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetServiceByIdQuery(id);
            var result = await _mediator.Send(query);

            if (!result.IsSucess)
            {
                return NotFound(result.Message);
            }

            return Ok(result);
        }


        // POST api/service
        [HttpPost]
        public async Task<IActionResult> PostService([FromBody] CreateServiceInputModel model)
        {
            var command = new InsertServiceCommand(
                model.Name,
                model.Description,
                model.Value,
                model.Duration
            );

            var result = await _mediator.Send(command);

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, model);
        }

        // PUT api/service
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(int id, [FromBody] UpdateServiceInputModel model)
        {
            if (model == null)
            {
                return BadRequest("Os dados do serviço são obrigatórios.");
            }

            var command = new UpdateServiceCommand(
                id,
                model.Name,
                model.Description,
                model.Value,
                model.Duration
            );

            var result = await _mediator.Send(command);

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        // DELETE api/service
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var command = new DeleteServiceCommand(id);
            var result = await _mediator.Send(command);

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }
    }
}
