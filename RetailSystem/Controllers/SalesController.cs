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
    [Route("api/Sales")]
    public class SalesController : Controller
    {
        private readonly IRepository<Sale> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SalesController(IRepository<Sale> repository)
        {
            _repository = repository;
        }

        // GET: api/Sales
        [HttpGet]
        public async Task<IEnumerable<Sale>> GetSales()
        {
            return await _repository.GetAsync();
        }

        // GET: api/Sales/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSale([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sale = await _repository.GetByIdAsync(id);

            if (sale == null)
            {
                return NotFound();
            }

            return Ok(sale);
        }

        // PUT: api/Sales/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSale([FromRoute] int id, [FromBody] Sale sale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sale.Id)
            {
                return BadRequest();
            }

            _repository.Update(sale);

            try
            {
                await _unitOfWork.SaveAsync();
            }

            catch (Exception)
            {
                if (!await _repository.Exists(sale.Id))
                {
                    return BadRequest("Sale does not exist");
                }

                throw new Exception("An unexpected error occured. Could not update.");
            }

            return NotFound();
        }

        // POST: api/Sales
        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] Sale sale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(sale);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction("GetSale", new { id = sale.Id }, sale);
        }

        // DELETE: api/Sales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale([FromRoute] int id)
        {
            var removed = await _repository.Remove(id);

            try
            {
                if (!removed)
                {
                    return NotFound("The Sale to be deleted was not found");
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