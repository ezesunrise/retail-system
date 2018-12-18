using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailSystem.Data;
using RetailSystem.Dtos;
using RetailSystem.Models;

namespace RetailSystem.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class CustomerController : Controller
    {
        private readonly IRepository<Customer> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerController(IRepository<Customer> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CustomerListDto>> GetAllCustomers()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerListDto>>(entities);
        }

        [HttpGet]
        public async Task<IEnumerable<CustomerListDto>> GetCustomers(int businessId)
        {
            var entities = await _repository.GetAsync(c => c.BusinessId == businessId);
            return _mapper.Map<IEnumerable<CustomerListDto>>(entities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            var entityDto = _mapper.Map<CustomerDto>(entity);
            return Ok(entityDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerDto entityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = _mapper.Map<Customer>(entityDto);
            try
            {
                _repository.Add(entity);
                await _unitOfWork.SaveAsync();
                return CreatedAtAction("GetCustomerById", new { id = entity.Id }, entity.Id);
            }
            catch (Exception e)
            {
                throw new Exception("An unexpected error occured. Could not be added.", e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] int id, [FromBody] CustomerDto entityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != entityDto.Id)
            {
                return BadRequest();
            }

            var entity = await _repository.GetByIdAsync(entityDto.Id);
            if (entity == null)
            {
                return NotFound("Customer does not exist");
            }

            _mapper.Map(entityDto, entity);

            try
            {
                _repository.Update(entity);
                await _unitOfWork.SaveAsync();
            }

            catch (Exception)
            {
                throw new Exception("An unexpected error occured. Could not update.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return BadRequest("The Customer to be deleted does not exist");
            }

            _repository.Remove(entity);

            try
            {
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