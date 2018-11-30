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
    [Route("api/ReportGroups")]
    public class ReportsController : Controller
    {
        private readonly IRepository<ReportGroup> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReportsController(IRepository<ReportGroup> repository)
        {
            _repository = repository;
        }

        // GET: api/ReportGroups
        [HttpGet]
        public async Task<IEnumerable<ReportGroup>> GetReportGroups()
        {
            return await _repository.GetAsync();
        }

        // GET: api/ReportGroups/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReportGroup([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reportGroup = await _repository.GetByIdAsync(id);

            if (reportGroup == null)
            {
                return NotFound();
            }

            return Ok(reportGroup);
        }

        // PUT: api/ReportGroups/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReportGroup([FromRoute] int id, [FromBody] ReportGroup reportGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reportGroup.Id)
            {
                return BadRequest();
            }

            _repository.Update(reportGroup);

            try
            {
                await _unitOfWork.SaveAsync();
            }

            catch (Exception)
            {
                if (!await _repository.Exists(reportGroup.Id))
                {
                    return BadRequest("Report does not exist");
                }

                throw new Exception("An unexpected error occured. Could not update.");
            }

            return NotFound();
        }

        // POST: api/ReportGroups
        [HttpPost]
        public async Task<IActionResult> CreateReportGroup([FromBody] ReportGroup reportGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(reportGroup);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction("GetReportGroup", new { id = reportGroup.Id }, reportGroup);
        }

        // DELETE: api/ReportGroups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReportGroup([FromRoute] int id)
        {
            var removed = await _repository.Remove(id);

            try
            {
                if (!removed)
                {
                    return NotFound("The Report to be deleted was not found");
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