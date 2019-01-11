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
    public class PurchaseOrderController : Controller
    {
        private readonly IRepository<PurchaseOrder> _repository;
        private readonly ICompositeRepository<LocationItem> _locationItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PurchaseOrderController(IRepository<PurchaseOrder> repository, ICompositeRepository<LocationItem> locationItemRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _locationItemRepository = locationItemRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<PurchaseOrderListDto>> GetPurchaseOrders(int locationId, DateTime? from, DateTime? to)
        {
            from = from ?? DateTime.Today;
            to = to ?? DateTime.Now;
            var entities = await _repository.GetAsync(s => s.LocationId == locationId && s.CreationTime >= from && s.CreationTime <= to);
            return _mapper.Map<IEnumerable<PurchaseOrderListDto>>(entities);
        }

        [HttpGet("{id}")]
        [SwaggerResponse(typeof(PurchaseOrderDto))]
        public async Task<IActionResult> GetPurchaseOrderById([FromRoute] int id)
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
        [SwaggerResponse(typeof(PurchaseOrderDto))]
        public async Task<IActionResult> CreatePurchaseOrder([FromBody] PurchaseOrderDto entityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = _mapper.Map<PurchaseOrder>(entityDto);
            _repository.Add(entity); // adds the purchase order to the repository

            // Get the location items involved in the transaction using the locationId of the purchase order and
            // the itemsIds of the purchase order items.
            var itemIds = entityDto.PurchaseOrderItems.Select(p => p.ItemId); 
            var locationItems = await _locationItemRepository
                .GetAsync(l => l.LocationId == entity.LocationId && itemIds.Contains(l.ItemId));

            if (locationItems.Count() != itemIds.Count())
            {
                return BadRequest("Duplicate item in purchase or an item does not exist in the specified location");
            }

            foreach (var lItem in locationItems)
            {
                lItem.Quantity += entity.PurchaseOrderItems.Single(p => p.ItemId == lItem.ItemId).Quantity;
                if (lItem.Quantity < 0)
                {
                    return BadRequest(new { message = "Quantity cannot be less than zero", lItem });
                }
                _locationItemRepository.Update(lItem);
            }

            try
            {
                await _unitOfWork.SaveAsync();
                var createdResult = CreatedAtAction("GetPurchaseById", new { id = entity.Id }, entity.Id);
                createdResult.StatusCode = 200;
                return createdResult;
            }
            catch (Exception e)
            {
                throw new Exception("An unexpected error occured. Could not be added.", e);
            }
        }

        [HttpPut("{id}")]
        [SwaggerResponse(typeof(PurchaseOrderDto))]
        public async Task<IActionResult> UpdatePurchaseOrder([FromRoute] int id, [FromBody] PurchaseOrderDto entityDto)
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

            return Ok(_mapper.Map<PurchaseOrderDto>(entity));
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(typeof(int))]
        public async Task<IActionResult> DeletePurchaseOrder([FromRoute] int id)
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
                return Ok(entity.Id);
            }
            catch (Exception)
            {
                throw new Exception("An unexpected error occured. Could not delete.");
            }
        }

    }
}