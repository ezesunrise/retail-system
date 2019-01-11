using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class LocationController : Controller
    {
        private readonly IRepository<Location> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LocationController(IRepository<Location> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<LocationListDto>> GetAllLocations(int businessId)
        {
            var entities = await _repository.GetAsync(l => l.BusinessId == businessId);
            return _mapper.Map<IEnumerable<LocationListDto>>(entities);
        }

        [HttpGet]
        [SwaggerResponse(typeof(LocationDto))]
        public async Task<IActionResult> GetLocationById([FromQuery] int? id)
        {
            if(id == null)
            {
                return Ok(new LocationDto());
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = await _repository.GetByIdAsync(id.Value);

            if (entity == null)
            {
                return NotFound();
            }

            var entityDto = _mapper.Map<LocationDto>(entity);
            return Ok(entityDto);
        }

        [HttpPost]
        [SwaggerResponse(typeof(LocationDto))]
        public async Task<IActionResult> CreateLocation([FromBody] LocationDto entityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = _mapper.Map<Location>(entityDto);
            try
            {
                _repository.Add(entity);
                await _unitOfWork.SaveAsync();
                var createdResult = CreatedAtAction("GetLocationById", new { id = entity.Id }, entity.Id);
                createdResult.StatusCode = 200;
                return createdResult;
            }
            catch (Exception e)
            {
                throw new Exception("An unexpected error occured. Could not be added.", e);
            }
        }

        [HttpPut("{id}")]
        [SwaggerResponse(typeof(LocationDto))]
        public async Task<IActionResult> UpdateLocation([FromRoute] int id, [FromBody] LocationDto entityDto)
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
                return BadRequest("Location does not exist");
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

            return Ok(_mapper.Map<LocationDto>(entity));
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(typeof(int))]
        public async Task<IActionResult> DeleteLocation([FromRoute] int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return BadRequest("The Location to be deleted does not exist");
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