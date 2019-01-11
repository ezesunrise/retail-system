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
    public class ItemController : Controller
    {
        private readonly IRepository<Item> _repository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Supplier> _supplierRepository;
        private readonly IRepository<Manufacturer> _manufacturerRepository;
        private readonly IRepository<Unit> _unitRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ItemController(
            IRepository<Item> repository,
            IRepository<Category> categoryRepository,
            IRepository<Supplier> supplierRepository,
            IRepository<Manufacturer> manufacturerRepository,
            IRepository<Unit> unitRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
            _manufacturerRepository = manufacturerRepository;
            _unitRepository = unitRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<KeyValuePairDto>[]> GetListItems(int bussinessId)
        {
            List<KeyValuePairDto>[] lists = new List<KeyValuePairDto>[3];

            var categories = await _categoryRepository
                .GetAsync(c => c.BusinessId == bussinessId);
            var categoryKeyValPairs = categories.Select(i => new KeyValuePairDto
            {
                DisplayName = i.Name,
                Value = i.Id
            });

            var suppliers = await _supplierRepository
                .GetAsync(c => c.BusinessId == bussinessId);
            var supplierKeyValPairs = suppliers.Select(i => new KeyValuePairDto
            {
                DisplayName = i.Name,
                Value = i.Id
            });

            var manufacturers = await _manufacturerRepository
                .GetAsync(c => c.BusinessId == bussinessId);
            var manufacturerKeyValPairs = categories.Select(i => new KeyValuePairDto
            {
                DisplayName = i.Name,
                Value = i.Id
            });

            var units = await _unitRepository
                .GetAsync(c => c.BusinessId == bussinessId);
            var unitKeyValPairs = categories.Select(i => new KeyValuePairDto
            {
                DisplayName = i.Name,
                Value = i.Id
            });

            lists.Append(categoryKeyValPairs);
            lists.Append(supplierKeyValPairs);
            lists.Append(manufacturerKeyValPairs);
            lists.Append(unitKeyValPairs);
            return lists;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemListDto>> GetAllItems(int businessId, string filter = "", string sorting = "", int maxResultCount = 100, int skipCount = 0)
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
        [SwaggerResponse(typeof(ItemDto))]
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
        [SwaggerResponse(typeof(ItemDto))]
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
                var createdResult = CreatedAtAction("GetItemById", new { id = entity.Id }, entity.Id);
                createdResult.StatusCode = 200;
                return createdResult;
            }
            catch (Exception e)
            {
                throw new Exception("An unexpected error occured. Could not be added.", e);
            }
        }

        [HttpPut("{id}")]
        [SwaggerResponse(typeof(ItemDto))]
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

            return Ok(_mapper.Map<ItemDto>(entity));
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(typeof(int))]
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
                return Ok(entity.Id);
            }
            catch (Exception)
            {
                throw new Exception("An unexpected error occured. Could not delete.");
            }
        }

    }
}