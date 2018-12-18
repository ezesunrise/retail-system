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
    public class TransferController : Controller
    {
        private readonly IRepository<Transfer> _repository;
        private readonly ICompositeRepository<LocationItem> _locationItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TransferController(IRepository<Transfer> repository, ICompositeRepository<LocationItem> locationItemRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _locationItemRepository = locationItemRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<TransferListDto>> GetAllTransfers(int businessId)
        {
            var entities = await _repository.GetAsync(t => t.SourceLocation.BusinessId == businessId);
            return _mapper.Map<IEnumerable<TransferListDto>>(entities);
        }

        [HttpGet]
        public async Task<IEnumerable<TransferListDto>> GetTransfers(int? sourceLocationId, int? destinationLocationId, DateTime? from, DateTime? to)
        {
            from = from ?? DateTime.Today;
            to = to ?? DateTime.Now;
            var entities = await _repository
                .GetAsync(t => 
                sourceLocationId.HasValue ? 
                    t.SourceLocationId == sourceLocationId : true
                && destinationLocationId.HasValue ? 
                    t.DestinationLocationId == destinationLocationId : true
                && t.CreationTime >= from && t.CreationTime <= to);

            return _mapper.Map<IEnumerable<TransferListDto>>(entities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransferById([FromRoute] int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            var entityDto = _mapper.Map<TransferDto>(entity);
            return Ok(entityDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransfer([FromBody] TransferDto entityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = _mapper.Map<Transfer>(entityDto);
            _repository.Add(entity);

            var itemIds = entityDto.TransferItems.Select(t => t.ItemId);
            var sourceLocationItems = await _locationItemRepository
                .GetAsync(l => l.LocationId == entity.SourceLocationId && itemIds.Contains(l.ItemId));

            if (sourceLocationItems.Count() != itemIds.Count())
            {
                return BadRequest("An item must have been duplicated in the transfer or does not exist in the source location");
            }

            var destinationLocationItems = await _locationItemRepository
                .GetAsync(l => l.LocationId == entity.DestinationLocationId && itemIds.Contains(l.ItemId));

            var newLocationItemIds = itemIds.Where(i => !destinationLocationItems.Select(l => l.ItemId).Contains(i));

            foreach (var item in sourceLocationItems)
            {
                item.Quantity -= entity.TransferItems.Single(t => t.ItemId == item.ItemId).Quantity;
                if (item.Quantity < 0)
                {
                    return BadRequest(new { message = "Quantity cannot be less than zero", item });
                }
                _locationItemRepository.Update(item);
            }

            foreach (var item in destinationLocationItems)
            {
                item.Quantity += entity.TransferItems.Single(t => t.ItemId == item.ItemId).Quantity;
                if (item.Quantity < 0)
                {
                    return BadRequest(new { message = "Quantity cannot be less than zero", item });
                }
                _locationItemRepository.Update(item);
            }

            var newLocationItems = entity.TransferItems.Where(t => newLocationItemIds.Contains(t.ItemId))
                .Select(t => new LocationItem() {
                    ItemId = t.ItemId,
                    Quantity = t.Quantity,
                    LocationId = entity.DestinationLocationId
                });
            _locationItemRepository.AddRange(newLocationItems);

            try
            {
                await _unitOfWork.SaveAsync();
                return CreatedAtAction("GetTransferById", new { id = entity.Id }, entity.Id);
            }
            catch (Exception e)
            {
                throw new Exception("An unexpected error occured. Could not be added.", e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransfer([FromRoute] int id, [FromBody] TransferDto entityDto)
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
                return NotFound("Transfer does not exist");
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
        public async Task<IActionResult> DeleteTransfer([FromRoute] int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return BadRequest("The Transfer to be deleted does not exist");
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