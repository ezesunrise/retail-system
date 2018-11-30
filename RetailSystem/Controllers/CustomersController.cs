using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailSystem.Data;
using RetailSystem.Models;

namespace RetailSystem.Controllers
{
    [Produces("application/json")]
    //[Route("api/Customers")]
    public class CustomersController : Controller
    {
        private readonly IRepository<Customer> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomersController(IRepository<Customer> customerRepository)
        {
            _repository = customerRepository;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _repository.GetAsync();
        }

        // GET: api/Customers/5
        [HttpGet]
        public async Task<IActionResult> GetCustomerById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _repository.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }
        
        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] int id, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.Id)
            {
                return BadRequest();
            }

            _repository.Update(customer);

            try
            {
                await _unitOfWork.SaveAsync();
            }

            catch (Exception)
            {
                if (!await _repository.Exists(customer.Id))
                {
                    return BadRequest("Customer does not exist");
                }

                throw new Exception("An unexpected error occured. Could not update.");
            }

            return NotFound();
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(customer);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            var removed = await _repository.Remove(id);

            try
            {
                if (!removed)
                {
                    return NotFound("The Customer to be deleted was not found");
                }
                await _unitOfWork.SaveAsync();
                return Ok();
            }
            catch (Exception)
            {
                throw new Exception("An unexpected error occured. Could not delete.");
            }
        }

    }
}