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
    public class PurchasesController : Controller
    {
        private readonly IRepository<PurchaseOrder> _repository;
        private readonly ICompositeRepository<LocationItem> _locationItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PurchasesController(IRepository<PurchaseOrder> repository, ICompositeRepository<LocationItem> locationItemRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _locationItemRepository = locationItemRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<PurchaseOrderListDto>> GetPurchases(int locationId, DateTime? from, DateTime? to)
        {
            from = from ?? DateTime.Today;
            to = to ?? DateTime.Now;
            var entities = await _repository.GetAsync(s => s.LocationId == locationId && s.CreationTime >= from && s.CreationTime <= to);
            return _mapper.Map<IEnumerable<PurchaseOrderListDto>>(entities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPurchaseById([FromRoute] int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            var entityDto = _mapper.Map<PurchaseOrderDto>(entity);
            return Ok(entityDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePurchase([FromBody] PurchaseOrderDto entityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = _mapper.Map<PurchaseOrder>(entityDto);
            _repository.Add(entity);

            var itemIds = entityDto.PurchaseOrderItems.Select(s => s.ItemId);
            var locationItems = await _locationItemRepository
                .GetAsync(l => l.LocationId == entity.LocationId && itemIds.Contains(l.ItemId));

            if (locationItems.Count() != itemIds.Count())
            {
                return BadRequest("Duplicate item in purchase or an item does not exist");
            }

            foreach (var item in locationItems)
            {
                item.Quantity += entity.PurchaseOrderItems.Single(s => s.ItemId == item.ItemId).Quantity;
                if (item.Quantity < 0)
                {
                    return BadRequest(new { message = "Quantity cannot be less than zero", item });
                }
                _locationItemRepository.Update(item);
            }

            try
            {
                await _unitOfWork.SaveAsync();
                return CreatedAtAction("GetPurchaseById", new { id = entity.Id }, entity.Id);
            }
            catch (Exception e)
            {
                throw new Exception("An unexpected error occured. Could not be added.", e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePurchase([FromRoute] int id, [FromBody] PurchaseOrderDto entityDto)
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
                return NotFound("Purchase does not exist");
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
        public async Task<IActionResult> DeletePurchase([FromRoute] int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return BadRequest("The Purchase to be deleted does not exist");
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