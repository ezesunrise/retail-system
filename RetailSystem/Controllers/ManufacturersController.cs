using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetailSystem.Data;
using RetailSystem.Dtos;
using RetailSystem.Models;

namespace RetailSystem.Controllers
{
    [Produces("application/json")]
    public class ManufacturersController : Controller
    {
        private readonly IRepository<Manufacturer> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ManufacturersController(IRepository<Manufacturer> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Manufacturers
        [HttpGet]
        public async Task<IEnumerable<ManufacturerDto>> GetManufacturers()
        {
            var manufacturersInDb = await _repository.GetAsync();
            return _mapper.Map<IEnumerable<Manufacturer>, IEnumerable<ManufacturerDto>>(manufacturersInDb);
        }

        // GET: api/Manufacturers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetManufacturer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var manufacturer = await _repository.GetByIdAsync(id);

            if (manufacturer == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Manufacturer, ManufacturerDto>(manufacturer));
        }

        // PUT: api/Manufacturers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateManufacturer([FromRoute] int id, [FromBody] Manufacturer manufacturer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != manufacturer.Id)
            {
                return BadRequest();
            }

            _repository.Update(manufacturer);

            try
            {
                await _unitOfWork.SaveAsync();
            }

            catch (Exception)
            {
                if (!await _repository.Exists(manufacturer.Id))
                {
                    return BadRequest("Manufacturer does not exist");
                }

                throw new Exception("An unexpected error occured. Could not update.");
            }

            return NotFound();
        }

        // POST: api/Manufacturers
        [HttpPost]
        public async Task<IActionResult> CreateManufacturer([FromBody] Manufacturer manufacturer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(manufacturer);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction("GetManufacturer", new { id = manufacturer.Id }, manufacturer);
        }

        // DELETE: api/Manufacturers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManufacturer([FromRoute] int id)
        {
            var removed = await _repository.Remove(id);

            try
            {
                if (!removed)
                {
                    return NotFound("The Manufacturer to be deleted was not found");
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