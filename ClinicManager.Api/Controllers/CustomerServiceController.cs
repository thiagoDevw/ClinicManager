using ClinicManager.Api.Models.CustomerModels;
using ClinicManager.Application.Commands.CommandsCustomerService.CreateCustomerService;
using ClinicManager.Application.Commands.CommandsCustomerService.DeleteCustomerService;
using ClinicManager.Application.Queries.QueriesCustomerService;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerServiceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/CustomerService
        [HttpGet]
        public async Task<IActionResult> GetAll(string search = ""/*, int page = 1, int pageSize = 3*/)
        {
            var query = new GetAllCustomerServiceQuery(search);
            var result = await _mediator.Send(query);

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Data);
        }

        // GetById api/customerService
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetCustomerServiceByIdQuery(id);
            var result = await _mediator.Send(query);

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Data);
        }

        // POST api/customerService
        [HttpPost]
        public async Task<IActionResult> PostCustomer(CreateCustomerInputModel model)
        {
            var command = new InsertCustomerServiceCommand(
                model.PatientId,
                model.DoctorId,
                model.ServiceId,
                model.Agreement,
                model.TypeService,
                model.Start,
                model.End
            );

            var result = await _mediator.Send(command);

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, model);
        }

        // PUT api/customerService
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, UpdateCustomerInputModel model)
        {
            var command = new InsertCustomerServiceCommand(
                model.PatientId,
                model.DoctorId,
                model.ServiceId,
                model.Agreement,
                model.TypeService,
                model.Start,
                model.End
            );

            var result = await _mediator.Send(command);

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        // DELETE api/customerService
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var command = new DeleteCustomerCommand(id);
            var result = await _mediator.Send(command);

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }
    }
}
