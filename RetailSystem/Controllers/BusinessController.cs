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
    public class BusinessesController : Controller
    {
        private readonly IRepository<Business> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BusinessesController(IRepository<Business> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<BusinessDto>> GetBusinesses()
        {
            var businesses = await _repository.GetAsync();
            return _mapper.Map<IEnumerable<BusinessDto>>(businesses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBusiness([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var business = await _repository.GetByIdAsync(id);

            if (business == null)
            {
                return NotFound();
            }

            var businessDto = _mapper.Map<BusinessDto>(business);
            return Ok(businessDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBusiness([FromRoute] int id, [FromBody] BusinessDto businessDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != businessDto.Id)
            {
                return BadRequest();
            }

            var business = _mapper.Map<Business>(businessDto);
            _repository.Update(business);

            try
            {
                await _unitOfWork.SaveAsync();
            }

            catch (Exception)
            {
                if (!await _repository.Exists(business.Id))
                {
                    return BadRequest("Business does not exist");
                }

                throw new Exception("An unexpected error occured. Could not update.");
            }

            return NoContent();
        }

        // POST: api/Businesses
        [HttpPost]
        public async Task<IActionResult> CreateBusiness([FromBody] BusinessDto businessDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var business = _mapper.Map<Business>(businessDto);
            try
            {
                _repository.Add(business);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception e)
            {
                throw new Exception("An unexpected error occured. Could not be added.", e);
            }

            return Created("GetBusinessById", new { id = business.Id });
        }

        // DELETE: api/Businesses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBusiness([FromRoute] int id)
        {
            var removed = await _repository.Remove(id);

            try
            {
                if (!removed)
                {
                    return NotFound("The Business to be deleted was not found");
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