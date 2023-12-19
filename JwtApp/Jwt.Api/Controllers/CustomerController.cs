using Jwt.Api.Dtos.CustomerDtos;
using Jwt.Api.Models.Entity;
using Jwt.Api.Repositories.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jwt.Api.Controllers;

[ApiController]
[Route("api/[controller]s/[action]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepository _customer;

    public CustomerController(ICustomerRepository customer)
    {
        _customer = customer;
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomers()
    {
        var customers = await _customer.GetAllAsync();
        return Ok(customers.OrderBy(user => user.Id).Select(customer => new CustomerDto(customer.Id, customer.FullName, customer.Age, customer.Email, customer.Phone)));
    }


    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetCustomerById(int customerId)
    {
        var result = await _customer.GetAsync(customer => customer.Id == customerId);
        if (result != null)
        {
            return Ok(new CustomerDto(result.Id, result.FullName, result.Age, result.Email, result.Phone));
        }
        return NotFound($"{customerId} Id customer not found!");
    }


    [HttpGet("{customerName}")]
    public async Task<IActionResult> GetCustomerByName(string customerName)
    {
        var result = await _customer.GetAsync(customer => customer.FullName.ToLower().Contains(customerName.ToLower()));
        if (result != null)
        {
            return Ok(new CustomerDto(result.Id, result.FullName, result.Age, result.Email, result.Phone));
        }
        return NotFound($"{customerName} customer not found!");
    }


    [Authorize(Roles = "SuperAdmin,Admin")]
    [HttpPost]
    public IActionResult CreateCustomer([FromBody] CreateCustomerDto createCustomer)
    {
        if (ModelState.IsValid)
        {
            Customer customer = new()
            {
                FullName = createCustomer.FullName,
                Age = createCustomer.Age,
                Email = createCustomer.Email,
                Phone = createCustomer.Phone
            };

            _customer.Add(customer);
            return Created("", createCustomer);
        }

        return BadRequest();

    }


    [Authorize(Roles = "SuperAdmin,Admin")]
    [HttpPut]
    public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerDto updateCustomer)
    {
        var customer = await _customer.GetAsync(customer => customer.Id == updateCustomer.Id);
        if (customer == null)
        {
            return NotFound("Customer not found!");
        }

        if (ModelState.IsValid)
        {
            customer.FullName = updateCustomer.FullName == default ? customer.FullName : updateCustomer.FullName;
            customer.Age = updateCustomer.Age == default ? customer.Age : updateCustomer.Age;
            customer.Email = updateCustomer.Email == default ? customer.Email : updateCustomer.Email;
            customer.Phone = updateCustomer.Phone == default ? customer.Phone : updateCustomer.Phone;

            _customer.Edit(customer);
            return NoContent();
        }

        return BadRequest();

    }


    [Authorize(Roles = "SuperAdmin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {

        var customer = await _customer.GetAsync(customer => customer.Id == id);
        if(customer == null)
        {
            return NotFound("Customer not found!");
        }

        _customer.Delete(customer);
        return NoContent();
    }



}