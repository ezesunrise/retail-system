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
    [Route("api/Purchases")]
    public class PurchasesController : Controller
    {
        private readonly IRepository<Purchase> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PurchasesController(IRepository<Purchase> repository)
        {
            _repository = repository;
        }

        // GET: api/Purchases
        [HttpGet]
        public async Task<IEnumerable<Purchase>> GetPurchases()
        {
            return await _repository.GetAsync();
        }

        // GET: api/Purchases/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPurchase([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var purchase = await _repository.GetByIdAsync(id);

            if (purchase == null)
            {
                return NotFound();
            }

            return Ok(purchase);
        }

        // PUT: api/Purchases/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePurchase([FromRoute] int id, [FromBody] Purchase purchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchase.Id)
            {
                return BadRequest();
            }

            _repository.Update(purchase);

            try
            {
                await _unitOfWork.SaveAsync();
            }

            catch (Exception)
            {
                if (!await _repository.Exists(purchase.Id))
                {
                    return BadRequest("Purchase does not exist");
                }

                throw new Exception("An unexpected error occured. Could not update.");
            }

            return NotFound();
        }

        // POST: api/Purchases
        [HttpPost]
        public async Task<IActionResult> CreatePurchase([FromBody] Purchase purchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(purchase);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction("GetPurchase", new { id = purchase.Id }, purchase);
        }

        // DELETE: api/Purchases/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchase([FromRoute] int id)
        {
            var removed = await _repository.Remove(id);

            try
            {
                if (!removed)
                {
                    return NotFound("The Purchase to be deleted was not found");
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