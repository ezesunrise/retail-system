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
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class SupplierController : Controller
    {
        private readonly IRepository<Supplier> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SupplierController(IRepository<Supplier> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerResponse(typeof(IEnumerable<SupplierListDto>))]
        public async Task<IEnumerable<SupplierListDto>> GetAllSuppliers()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<SupplierListDto>>(entities);
        }

        [HttpGet]
        [SwaggerResponse(typeof(IEnumerable<SupplierListDto>))]
        public async Task<IActionResult> GetSuppliers(int businessId)
        {
            var entities = await _repository.GetAsync(s => s.BusinessId == businessId);
            return Ok(_mapper.Map<IEnumerable<SupplierListDto>>(entities));
        }

        [HttpGet("{id}")]
        [SwaggerResponse(typeof(SupplierDto))]
        public async Task<IActionResult> GetSupplierById([FromRoute] int id)
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

            var entityDto = _mapper.Map<SupplierDto>(entity);
            return Ok(entityDto);
        }

        [HttpPost]
        [SwaggerResponse(typeof(SupplierDto))]
        public async Task<IActionResult> CreateSupplier([FromBody] SupplierDto entityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = _mapper.Map<Supplier>(entityDto);
            try
            {
                _repository.Add(entity);
                await _unitOfWork.SaveAsync();
                var createdResult = CreatedAtAction("GetSupplierById", new { id = entity.Id }, _mapper.Map<SupplierDto>(entity));
                createdResult.StatusCode = 200;
                return createdResult;
            }
            catch (Exception e)
            {
                throw new Exception("An unexpected error occured. Could not be added.", e);
            }
        }

        [HttpPut("{id}")]
        [SwaggerResponse(typeof(SupplierDto))]
        public async Task<IActionResult> UpdateSupplier([FromRoute] int id, [FromBody] SupplierDto entityDto)
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
                return NotFound("Supplier does not exist");
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

            return Ok(_mapper.Map<SupplierDto>(entity));
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(typeof(int))]
        public async Task<IActionResult> DeleteSupplier([FromRoute] int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return BadRequest("The Supplier to be deleted does not exist");
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