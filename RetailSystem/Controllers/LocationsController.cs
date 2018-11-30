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
    [Route("api/Locations")]
    public class LocationsController : Controller
    {
        private readonly IRepository<Location> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LocationsController(IRepository<Location> repository)
        {
            _repository = repository;
        }

        // GET: api/Locations
        [HttpGet]
        public async Task<IEnumerable<Location>> GetLocations()
        {
            return await _repository.GetAsync();
        }

        // GET: api/Locations/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var location = await _repository.GetByIdAsync(id);

            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        // PUT: api/Locations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocation([FromRoute] int id, [FromBody] Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != location.Id)
            {
                return BadRequest();
            }

            _repository.Update(location);

            try
            {
                await _unitOfWork.SaveAsync();
            }

            catch (Exception)
            {
                if (!await _repository.Exists(location.Id))
                {
                    return BadRequest("Location does not exist");
                }

                throw new Exception("An unexpected error occured. Could not update.");
            }

            return NotFound();
        }

        // POST: api/Locations
        [HttpPost]
        public async Task<IActionResult> CreateLocation([FromBody] Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(location);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction("GetLocation", new { id = location.Id }, location);
        }

        // DELETE: api/Locations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation([FromRoute] int id)
        {
            var removed = await _repository.Remove(id);

            try
            {
                if (!removed)
                {
                    return NotFound("The Location to be deleted was not found");
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