using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RetailSystem.Data;
using RetailSystem.Dtos;
using RetailSystem.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataCapture.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]/[action]")]
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

        [AllowAnonymous]
        [SwaggerResponse(typeof(string))]
        [HttpPost]
        public IActionResult Authenticate(string userName, string password )
        {
            var user = _userService.Authenticate(userName, password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user.Token);
        }

        // POST: api/Users/CreateUser
        [SwaggerResponse(typeof(int))]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] RegisterDto data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
            var entities = await _userService.GetAllAsync();
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

        // POST: /Account/ResetPassword/5
        //[HttpPost]
        //[Route("api/users/resetPassword/{id}")]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> ResetPassword(string id, ResetPasswordDto model)
        //{
        //    if (id != model.UserId)
        //    {
        //        return BadRequest();
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var adminUser = await _manager.Fin_contextyNameAsync(User.Iduser.Name);
        //    if (adminUser == null)
        //    {
        //        // Don't reveal that the user does not exist
        //        return BadRequest();
        //    }
        //    var passResult = await _manager.CheckPasswordAsync(adminUser, model.AdminPassword);
        //    if (!passResult)
        //    {
        //        return BadRequest("Invalid admin password");
        //    }
        //    var user = await _manager.Fin_contextyIdAsync(id);
        //    if (user == null)
        //    {
        //        return BadRequest();
        //    }
        //    user.PasswordHash = new PasswordHasher().HashPassword(model.Password ?? "password");

        //    try
        //    {
        //        await _manager.UpdateAsync(user);
        //    }
        //    catch (_contextUpdateConcurrencyException ex)
        //    {
        //        throw new _contextUpdateConcurrencyException(ex.Message);
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}
        
        // DELETE: api/Users/DeleteUser/5
        [SwaggerResponse(typeof(int))]
        [HttpDelete("{id}")]
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