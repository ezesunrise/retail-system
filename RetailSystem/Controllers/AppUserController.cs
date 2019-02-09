using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RetailSystem.Data;
using RetailSystem.Dtos;
using RetailSystem.Models;
using RetailSystem.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataCapture.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]/[action]")]
    [Authorize(Roles = Role.AdminOrManager)]
    public class AppUserController : Controller
    {
        private IAppUserService _userService;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public AppUserController(IAppUserService userService, IUnitOfWork unitOfWwork, IMapper mapper)
        {
            _userService = userService;
            _unitOfWork = unitOfWwork;
            _mapper = mapper;
        }
        
        // POST: api/Users/CreateUser
        [SwaggerResponse(typeof(int))]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] RegisterDto data)
        {
            try
            {
                var user =_userService.Create(data);
                await _unitOfWork.SaveAsync();
                return CreatedAtAction("GetUserById", new { id = user.Id }, user.Id);
            }
            catch (Exception e)
            {
                throw new Exception("An unexpected error occured. Could not be added.", e);
            }
        }

        // GET: api/Users
        [SwaggerResponse(typeof(IEnumerable<AppUserListDto>))]
        [HttpGet]
        public async Task<IEnumerable<AppUserListDto>> GetUsers(string filter = "", string sorting = "", int maxResultCount = 50, int skipCount = 0)
        {
            var entities = await _userService
                .GetAsync(User?.HasClaim(c => c.Type == "business") ?? false
                ? int.Parse(User.FindFirst(c => c.Type == "business").Value) : 0,
                User?.HasClaim(c => c.Type == "location") ?? false
                ? int.Parse(User.FindFirst(c => c.Type == "location").Value) : 0);
            return _mapper.Map<IEnumerable<AppUserListDto>>(entities);
        }

        // GET: api/Users/5
        [SwaggerResponse(typeof(AppUserDto))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = _mapper.Map<AppUserDto>(user);
            return Ok(userDto);
        }

        // PUT: api/users/updateUser/5
        [SwaggerResponse(typeof(AppUserDto))]
        [HttpPut("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> UpdateUser(int id,[FromBody] AppUserDto userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.GetByIdAsync(userDto.Id);
            if (user == null)
            {
                return NotFound("User does not exist");
            }

            _mapper.Map(userDto, user);

            try
            {
                _userService.Update(user);
                await _unitOfWork.SaveAsync();
            }

            catch (Exception)
            {
                throw new Exception("An unexpected error occured. Could not update.");
            }

            return Ok(_mapper.Map<AppUserDto>(user));
        }

        // POST: /Account/ResetPassword
        [HttpPost]
        [SwaggerResponse(typeof(void))]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> ResetPassword()
        {
            var user = await _userService.GetByIdAsync(int.Parse(User.Identity.Name));
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return BadRequest();
            }
            _userService.ResetPassword(user);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        // DELETE: api/Users/DeleteUser/5
        [SwaggerResponse(typeof(int))]
        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return BadRequest("The User to be deleted does not exist");
            }

            _userService.Delete(user);

            try
            {
                await _unitOfWork.SaveAsync();
                return Ok(user.Id);
            }
            catch (Exception)
            {
                throw new Exception("An unexpected error occured. Could not delete.");
            }
        }
    }
}