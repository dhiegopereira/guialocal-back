using Microsoft.AspNetCore.Mvc;
using guialocal.Models;
using guialocal.Services;
using FluentValidation;

namespace guialocal.Controllers
{
    [Route("customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;
        private readonly IValidator<Customer> _validator;


        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
            //_validator = validator;
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            var validationResult = _validator.Validate(customer);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            try
            {                
                var createdCustomer = _customerService.Create(customer);
                return Ok(createdCustomer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult ReadAllByFilter()
        {
            try
            {
                var customers = _customerService.ReadAllByFilter("");
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{email}")]
        public IActionResult Update(string email, Customer customerData)
        {
            try
            {
                var updatedCustomer = _customerService.Update(email, customerData);
                return Ok(updatedCustomer);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{email}")]
        public IActionResult Delete(string email)
        {
            try
            {
                _customerService.Delete(email);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
