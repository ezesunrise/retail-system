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
    public class SupplyController : Controller
    {
        private readonly IRepository<Supply> _repository;
        private readonly ICompositeRepository<LocationItem> _locationItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SupplyController(IRepository<Supply> repository, ICompositeRepository<LocationItem> locationItemRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _locationItemRepository = locationItemRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<SupplyListDto>> GetSupplies(int locationId, DateTime? from, DateTime? to)
        {
            from = from ?? DateTime.Today;
            to = to ?? DateTime.Now;
            var entities = await _repository.GetAsync(s => s.LocationId == locationId && s.CreationTime >= from && s.CreationTime <= to);
            return _mapper.Map<IEnumerable<SupplyListDto>>(entities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplyById([FromRoute] int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            var entityDto = _mapper.Map<SupplyDto>(entity);
            return Ok(entityDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSupply([FromBody] SupplyDto entityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = _mapper.Map<Supply>(entityDto);
            _repository.Add(entity);

            var itemIds = entityDto.SupplyItems.Select(s => s.ItemId);
            var locationItems = await _locationItemRepository
                .GetAsync(l => l.LocationId == entity.LocationId && itemIds.Contains(l.ItemId));

            if (locationItems.Count() != itemIds.Count())
            {
                return BadRequest("Duplicate item in supply list or an item does not exist");
            }

            foreach (var item in locationItems)
            {
                item.Quantity -= entity.SupplyItems.Single(s => s.ItemId == item.ItemId).Quantity;
                if (item.Quantity < 0)
                {
                    return BadRequest(new { message = "Insufficient quantity available", item });
                }
                _locationItemRepository.Update(item);
            }

            try
            {
                await _unitOfWork.SaveAsync();
                return CreatedAtAction("GetSupplyById", new { id = entity.Id }, entity.Id);
            }
            catch (Exception e)
            {
                throw new Exception("An unexpected error occured. Could not be added.", e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupply([FromRoute] int id, [FromBody] SupplyDto entityDto)
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
                return NotFound("Supply does not exist");
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
        public async Task<IActionResult> DeleteSupply([FromRoute] int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return BadRequest("The Supply to be deleted does not exist");
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