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
    [Route("api/Transfers")]
    public class TransfersController : Controller
    {
        private readonly IRepository<Transfer> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TransfersController(IRepository<Transfer> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Transfers
        [HttpGet]
        public async Task<IEnumerable<Transfer>> GetTransfers()
        {
            return await _repository.GetAsync();
        }

        // GET: api/Transfers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransfer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transfer = await _repository.GetByIdAsync(id);

            if (transfer == null)
            {
                return NotFound();
            }

            return Ok(transfer);
        }

        // PUT: api/Transfers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransfer([FromRoute] int id, [FromBody] Transfer transfer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transfer.Id)
            {
                return BadRequest();
            }

            _repository.Update(transfer);

            try
            {
                await _unitOfWork.SaveAsync();
            }

            catch (Exception)
            {
                if (!await _repository.Exists(transfer.Id))
                {
                    return BadRequest("Transfer does not exist");
                }

                throw new Exception("An unexpected error occured. Could not update.");
            }

            return NotFound();
        }

        // POST: api/Transfers
        [HttpPost]
        public async Task<IActionResult> CreateTransfer([FromBody] Transfer transfer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(transfer);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction("GetTransfer", new { id = transfer.Id }, transfer);
        }

        // DELETE: api/Transfers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransfer([FromRoute] int id)
        {
            var removed = await _repository.Remove(id);

            try
            {
                if (!removed)
                {
                    return NotFound("The Transfer to be deleted was not found");
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