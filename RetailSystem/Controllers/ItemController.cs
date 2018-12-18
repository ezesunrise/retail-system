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
    public class ItemController : Controller
    {
        private readonly IRepository<Item> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ItemController(IRepository<Item> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemListDto>> GetAllItems(int businessId)
        {
            var entities = await _repository.GetAsync(i => i.Category.BusinessId == businessId);
            return _mapper.Map<IEnumerable<ItemListDto>>(entities);
        }

        [HttpGet]
        public async Task<IEnumerable<ItemListDto>> GetItemsByCategory(int categoryId)
        {
            var entities = await _repository.GetAsync(i => i.CategoryId == categoryId);
            return _mapper.Map<IEnumerable<ItemListDto>>(entities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemById([FromRoute] int id)
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

            var entityDto = _mapper.Map<ItemDto>(entity);
            return Ok(entityDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] ItemDto entityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = _mapper.Map<Item>(entityDto);
            try
            {
                _repository.Add(entity);
                await _unitOfWork.SaveAsync();
                return CreatedAtAction("GetItemById", new { id = entity.Id }, entity.Id);
            }
            catch (Exception e)
            {
                throw new Exception("An unexpected error occured. Could not be added.", e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem([FromRoute] int id, [FromBody] ItemDto entityDto)
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
                return NotFound("Item does not exist");
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
        public async Task<IActionResult> DeleteItem([FromRoute] int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return BadRequest("The Item to be deleted does not exist");
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