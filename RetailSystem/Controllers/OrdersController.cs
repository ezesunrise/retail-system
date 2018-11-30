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
    [Route("api/Orders")]
    public class OrdersController : Controller
    {
        private readonly IRepository<Order> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrdersController(IRepository<Order> repository)
        {
            _repository = repository;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _repository.GetAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _repository.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder([FromRoute] int id, [FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.Id)
            {
                return BadRequest();
            }
            _repository.Update(order);

            try
            {
                await _unitOfWork.SaveAsync();
            }

            catch (Exception)
            {
                if (!await _repository.Exists(order.Id))
                {
                    return BadRequest("Order does not exist");
                }

                throw new Exception("An unexpected error occured. Could not update.");
            }

            return NotFound();
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(order);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            var removed = await _repository.Remove(id);

            try
            {
                if (!removed)
                {
                    return NotFound("The Order to be deleted was not found");
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