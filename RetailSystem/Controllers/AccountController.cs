﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailSystem.Data;
using RetailSystem.Dtos;
using AutoMapper;
using NSwag.Annotations;
using RetailSystem.Services;
using System.Threading.Tasks;

namespace DataCapture.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private IAccountService _accountService;
        private IAppUserService _userService;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IAppUserService userService, IUnitOfWork unitOfWwork, IMapper mapper)
        {
            _accountService = accountService;
            _userService = userService;
            _unitOfWork = unitOfWwork;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [SwaggerResponse(typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthDto authDto)
        {
            var user = await _accountService.AuthenticateAsync(authDto);

            if (user == null)
                return BadRequest(new { message = "Incorrect username or password" });

            return Ok(user.Token);
        }

        // POST: /Account/ChangePassword
        [HttpPost]
        [SwaggerResponse(typeof(void))]
	[Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            var user = await _userService.GetByIdAsync(int.Parse(User.Identity.Name));
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return BadRequest();
            }
            //check password validity
            if (!AccountService.VerifyPasswordHash(dto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Incorrect password");
            }

            _accountService.ChangePassword(user, dto.NewPassword);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
    }
}
