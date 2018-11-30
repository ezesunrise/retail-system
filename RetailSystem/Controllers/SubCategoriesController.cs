using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailSystem.Data;
using RetailSystem.Models;

namespace RetailSystem.Controllers
{
    [Produces("application/json")]
    [Route("api/SubCategories")]
    public class SubCategoriesController : Controller
    {
        private readonly IRepository<SubCategory> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubCategoriesController(IRepository<SubCategory> repository)
        {
            _repository = repository;
        }

        // GET: api/SubCategories
        [HttpGet]
        public async Task<IEnumerable<SubCategory>> GetSubCategories()
        {
            return await _repository.GetAsync();
        }

        // GET: api/SubCategories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subCategory = await _repository.GetByIdAsync(id);

            if (subCategory == null)
            {
                return NotFound();
            }

            return Ok(subCategory);
        }

        // PUT: api/SubCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubCategory([FromRoute] int id, [FromBody] SubCategory subCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subCategory.Id)
            {
                return BadRequest();
            }

            _repository.Update(subCategory);

            try
            {
                await _unitOfWork.SaveAsync();
            }

            catch (Exception)
            {
                if (!await _repository.Exists(subCategory.Id))
                {
                    return BadRequest("Sub-category does not exist");
                }

                throw new Exception("An unexpected error occured. Could not update.");
            }

            return NotFound();
        }

        // POST: api/SubCategories
        [HttpPost]
        public async Task<IActionResult> CreateSubCategory([FromBody] SubCategory subCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(subCategory);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction("GetSubCategory", new { id = subCategory.Id }, subCategory);
        }

        // DELETE: api/SubCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategory([FromRoute] int id)
        {
            var removed = await _repository.Remove(id);

            try
            {
                if (!removed)
                {
                    return NotFound("The Sub-category to be deleted was not found");
                }
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