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
        private readonly IItemRepository _repository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Supplier> _supplierRepository;
        private readonly IRepository<Manufacturer> _manufacturerRepository;
        private readonly IRepository<Unit> _unitRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ItemController(
            IItemRepository repository,
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
        public async Task<IList<KeyValuePairDto>[]> GetListItems(int bussinessId)
        {
            List<KeyValuePairDto>[] lists = new List<KeyValuePairDto>[4];

            var categories = await _categoryRepository
                .GetAsync(c => c.BusinessId == bussinessId);
            var categoryKeyValPairs = categories.Select(i => new KeyValuePairDto
            {
                DisplayName = i.Name,
                Value = i.Id
            }).ToList();

            var suppliers = await _supplierRepository
                .GetAsync(s => s.BusinessId == bussinessId);
            var supplierKeyValPairs = suppliers.Select(i => new KeyValuePairDto
            {
                DisplayName = i.Name,
                Value = i.Id
            }).ToList();
            
            var manufacturers = await _manufacturerRepository
                .GetAsync(m => m.BusinessId == bussinessId);
            var manufacturerKeyValPairs = manufacturers.Select(i => new KeyValuePairDto
            {
                DisplayName = i.Name,
                Value = i.Id
            }).ToList();

            var units = await _unitRepository
                .GetAsync(u => u.BusinessId == bussinessId);
            var unitKeyValPairs = units.Select(i => new KeyValuePairDto
            {
                DisplayName = i.Name,
                Value = i.Id
            }).ToList();

            lists[0] = categoryKeyValPairs;
            lists[1] = supplierKeyValPairs;
            lists[2] = manufacturerKeyValPairs;
            lists[3] = unitKeyValPairs;
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
            var lastEntity = await _repository.GetLastAsync(i => i.CategoryId == entity.CategoryId);

            if(lastEntity == null)
            {
                var category = await _categoryRepository.GetByIdAsync(entity.CategoryId);
                if(category == null)
                {
                    return BadRequest("Category does not exist");
                }
                entity.Code = category.Code + "001";
            }
            else
            {
                var lastNumber = lastEntity.Code.Substring(AppConsts.CategoryCodeLength);
                var newNumber = (int.Parse(lastNumber) + 1).ToString();
                switch (newNumber.Length)
                {
                    case 1:
                        newNumber = "00" + newNumber;
                        break;
                    case 2:
                        newNumber = "0" + newNumber;
                        break;
                    default:
                        return BadRequest("Cannot add any more item to this category");
                }
                entity.Code = lastEntity.Code.Substring(0, AppConsts.CategoryCodeLength) + newNumber;
            }

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