using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSwag.Annotations;
using RetailSystem.Data;
using RetailSystem.Dtos;
using RetailSystem.Models;

namespace RetailSystem.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [Authorize(Roles = Role.AnyAdmin)]
    public class BusinessController : Controller
    {
        private readonly IRepository<Business> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BusinessController(IRepository<Business> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<BusinessDto>> GetBusinesses()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<BusinessDto>>(entities);
        }

        [HttpGet("{id}")]
        [SwaggerResponse(typeof(BusinessDto))]
        public async Task<IActionResult> GetBusinessById([FromRoute] int id)
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

            var entityDto = _mapper.Map<BusinessDto>(entity);
            return Ok(entityDto);
        }

        [HttpPost]
        [SwaggerResponse(typeof(BusinessDto))]
        [Authorize(Roles = Role.SuperAdmin)]
        public async Task<IActionResult> CreateBusiness([FromBody] BusinessDto entityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = _mapper.Map<Business>(entityDto);
            try
            {
                _repository.Add(entity);
                await _unitOfWork.SaveAsync();
                var createdResult = CreatedAtAction("GetBusinessById", new { id = entity.Id }, entity.Id);
                createdResult.StatusCode = 201;
                return createdResult;
            }
            catch (Exception e)
            {
                throw new Exception("An unexpected error occured. Could not be added.", e);
            }
        }

        [HttpPut("{id}")]
        [SwaggerResponse(typeof(BusinessDto))]
        [Authorize(Roles = Role.SuperAdmin)]
        public async Task<IActionResult> UpdateBusiness([FromRoute] int id, [FromBody] BusinessDto entityDto)
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
                return NotFound("Business does not exist");
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

            return Ok(_mapper.Map<BusinessDto>(entity));
        }
        
        [HttpDelete("{id}")]
        [SwaggerResponse(typeof(int))]
        [Authorize(Roles = Role.SuperAdmin)]
        public async Task<IActionResult> DeleteBusiness([FromRoute] int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return BadRequest("The Business to be deleted does not exist");
            }

            _repository.Remove(entity);

            try
            {
                await _unitOfWork.SaveAsync();
                return Ok(entity.Id);
            }
            catch (Exception)
            {
                throw new Exception("An unexpected error occured. Could not delete.");
            }
        }

    }
}