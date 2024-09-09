using ClinicManager.Api.Models.CustomerModels;
using ClinicManager.Application.Services.ServicesCustomer;
using ClinicManager.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerServiceController : ControllerBase
    {
        private readonly ICustomerService _service;
        public CustomerServiceController( ICustomerService service)
        {
            _service = service;
        }

        // GET: api/CustomerService
        [HttpGet]
        public IActionResult GetAll(string search = ""/*, int page = 1, int pageSize = 3*/)
        {
            var result = _service.GetAll();

            return Ok(result);
        }

        // GetById api/customerService
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

        // POST api/customerService
        [HttpPost]
        public async Task<IActionResult> PostCustomer(CreateCustomerInputModel model)
        {
            var result = await _service.Insert(model);


            return CreatedAtAction(nameof(GetById), new { id = result.Data }, model);
        }

        // PUT api/customerService
        [HttpPut("{id}")]
        public IActionResult PutCustomer(int id, UpdateCustomerInputModel model)
        {
            var result = _service.Update(model);

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        // DELETE api/customerService
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
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
