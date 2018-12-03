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
    public class LocationItemsController : Controller
    {
        private readonly ICompositeRepository<LocationItem> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LocationItemsController(ICompositeRepository<LocationItem> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<LocationItemListDto>> GetLocationItems(int? locationId, int? itemId)
        {
            if(!(locationId.HasValue || itemId.HasValue))
            {
                return null;
            }

            var entities = await _repository
                .GetAsync(i => locationId.HasValue ? i.LocationId == locationId : i.ItemId == itemId);

            return _mapper.Map<IEnumerable<LocationItemListDto>>(entities);
        }

        [HttpGet]
        public async Task<IActionResult> GetLocationItemById(int locationId, int itemId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var locationItem = await _repository.GetByIdAsync(locationId, itemId);

            if (locationItem == null)
            {
                return NotFound();
            }

            var locationItemDto = _mapper.Map<LocationItemDto>(locationItem);
            return Ok(locationItemDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocationItem([FromBody] LocationItemDto locationItemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var locationItem = _mapper.Map<LocationItem>(locationItemDto);
            try
            {
                _repository.Add(locationItem);
                await _unitOfWork.SaveAsync();
                return CreatedAtAction("GetLocationItemById", new { locationId = locationItem.LocationId, itemId = locationItem.ItemId }, null);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLocationItem([FromBody] LocationItemDto locationItemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var locationItem = await _repository.GetByIdAsync(locationItemDto.LocationId, locationItemDto.ItemId);
            if (locationItem == null)
            {
                return NotFound("Location Item does not exist");
            }

            _mapper.Map(locationItemDto, locationItem);

            try
            {
                _repository.Update(locationItem);
                await _unitOfWork.SaveAsync();
            }

            catch (Exception)
            {
                throw new Exception("An unexpected error occured. Could not update.");
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLocationItem(int locationId, int itemId)
        {
            var locationItem = await _repository.GetByIdAsync(locationId, itemId);
            if (locationItem == null)
            {
                return NotFound("The Location Item to be deleted does not exist");
            }

            _repository.Remove(locationItem);

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