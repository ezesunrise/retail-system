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
    public class SubCategoryController : Controller
    {
        private readonly IRepository<SubCategory> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubCategoryController(IRepository<SubCategory> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<SubCategoryListDto>> GetAllSubCategories()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<SubCategoryListDto>>(entities);
        }

        [HttpGet]
        public async Task<IEnumerable<SubCategoryListDto>> GetSubCategories(int categoryId)
        {
            var entities = await _repository.GetAsync(s => s.CategoryId == categoryId);
            return _mapper.Map<IEnumerable<SubCategoryListDto>>(entities);
        }

        [HttpGet("{id}")]
        [SwaggerResponse(typeof(SubCategoryDto))]
        public async Task<IActionResult> GetSubCategoryById([FromRoute] int id)
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

            var entityDto = _mapper.Map<SubCategoryDto>(entity);
            return Ok(entityDto);
        }

        [HttpPost]
        [SwaggerResponse(typeof(SubCategoryDto))]
        public async Task<IActionResult> CreateSubCategory([FromBody] SubCategoryDto entityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = _mapper.Map<SubCategory>(entityDto);
            try
            {
                _repository.Add(entity);
                await _unitOfWork.SaveAsync();
                var createdResult = CreatedAtAction("GetSubCategoryById", new { id = entity.Id }, entity.Id);
                createdResult.StatusCode = 200;
                return createdResult;
            }
            catch (Exception e)
            {
                throw new Exception("An unexpected error occured. Could not be added.", e);
            }
        }

        [HttpPut("{id}")]
        [SwaggerResponse(typeof(SubCategoryDto))]
        public async Task<IActionResult> UpdateSubCategory([FromRoute] int id, [FromBody] SubCategoryDto entityDto)
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
                return NotFound("Sub-category does not exist");
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

            return Ok(_mapper.Map<SubCategoryDto>(entity));
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(typeof(int))]
        public async Task<IActionResult> DeleteSubCategory([FromRoute] int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return BadRequest("The SubCategory to be deleted does not exist");
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