using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RetailSystem.Data;
using RetailSystem.Dtos;
using RetailSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DataCapture.Controllers
{
    public class AccountController : Controller
    {
        UserStore<AppUser> _userStore;
        UserManager<AppUser> _manager;
        //public AccountController()
        //{
        //    _userStore = new UserStore<AppUser>(new A);
        //    _manager = new UserManager<AppUser>(_userStore);
        //}

        //[Route("api/account/Register")]
        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> Register(RegisterDto model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var user = new AppUser() { UserName = model.UserName, Email = model.Email };
        //    user.Name = model.Name;
        //    _manager.PasswordValidator = new PasswordValidator
        //    {
        //        RequiredLength = 6
        //    };
        //    IdentityResult result = _manager.CreateAsync(user, model.Password);
        //    await _manager.AddToRoleAsync(user, "Registerer");
        //    return Created("DefaultApi", result);
        //}

        [HttpPost]
        [Route("api/account/Authenticate")]
        public IActionResult Authenticate()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = claimsIdentity.Claims;

            var model = new
            {
                UserName = claimsIdentity.FindFirst("Username").Value,
                Email = claimsIdentity.FindFirst("Email").Value,
                FullName = claimsIdentity.FindFirst("FullName").Value,
                LoggedOn = claimsIdentity.FindFirst("LoggedOn").Value,
                Roles = claimsIdentity.FindAll("Role").Select(r => r.Value)
            };
            return Ok(model);
        }

        // POST: /Account/ChangePassword
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _manager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return BadRequest();
            }
            var result = await _manager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Route("api/account/UpdateProfile")]
        [HttpPut]
        public async Task<IActionResult> UpdateCurrentUser(RegisterDto model)
        {
            var userId = User.Identity.Name;
            if (userId == null)
            {
                return BadRequest();
            }
            var userInDb = _userStore.Users.FirstOrDefault(u => u.Id == userId);

            if(!string.IsNullOrEmpty(model.Name)) userInDb.Name = model.Name;
            if(!string.IsNullOrEmpty(model.Email)) userInDb.Email = model.Email;
            if(!string.IsNullOrEmpty(model.Password)) userInDb.PasswordHash = new PasswordHasher<AppUser>().HashPassword(userInDb, model.Password);

            IdentityResult result = await _manager.UpdateAsync(userInDb);
            return Ok(result);
        }

    }
}
