using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RetailSystem.Data;
using RetailSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace DataCapture.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AppUserController : Controller
    {
        private ApplicationDbContext _context;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        UserStore<AppUser> _userStore;
        UserManager<AppUser> _manager;
        public AppUserController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _userStore = new UserStore<AppUser>(_context);
            //_manager = new UserManager<AppUser>(_userStore);
            _mapper = mapper;
        }

        //// POST: api/Users/CreateUser
        //public async Task<IActionResult> CreateUser(RegisterDto model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var user = new IdentityUser() { UserName = model.Username, Email = model.Email, Name = model.FullName, PhoneNumber = model.PhoneNumber };
        //    _manager.PasswordValidator = new PasswordValidator
        //    {
        //        RequiredLength = 6
        //    };
        //    _userStore.CreateAsync

        //    IdentityResult result = _manager.Create(user, model.Password);
        //    if (model.Roles != null && model.Roles.Length > 0)
        //    {
        //        await _manager.AddToRolesAsync(user.Id, model.Roles);
        //    }
        //    else
        //    {
        //        await _manager.AddToRoleAsync(user.Id, "Registerer");
        //    };

        //    return Created("DefaultApi", new { user.Id });
        //}

        //// GET: api/Users
        //public async Task<IList<UserListDto>> GetUsers(string filter = "", string sorting = "", int maxResultCount = 50, int skipCount = 0)
        //{
        //    IQueryable<IdentityUser> usersInDb;
        //    switch (sorting)
        //    {
        //        case "fullName":
        //            usersInDb = db.Users
        //                .Where(u => u.FullName.Contains(filter) || u.UserName.Contains(filter))
        //                .OrderBy(u => u.FullName)
        //                .Skip(skipCount)
        //                .Take(maxResultCount);
        //            break;
        //        case "fullName_Desc":
        //            usersInDb = db.Users
        //                .Where(u => u.FullName.Contains(filter) || u.UserName.Contains(filter))
        //                .OrderByDescending(u => u.FullName)
        //                .Skip(skipCount)
        //                .Take(maxResultCount);
        //            break;
        //        case "userName_Desc":
        //            usersInDb = db.Users
        //                .Where(u => u.FullName.Contains(filter) || u.UserName.Contains(filter))
        //                .OrderByDescending(u => u.UserName)
        //                .Skip(skipCount)
        //                .Take(maxResultCount);
        //            break;
        //        default:
        //            usersInDb = db.Users
        //                .Where(u => u.FullName.Contains(filter) || u.UserName.Contains(filter))
        //                .OrderBy(u => u.UserName)
        //                .Skip(skipCount)
        //                .Take(maxResultCount);
        //            break;
        //    }
        //    return await usersInDb
        //                .Include(u => u.Roles)
        //                .Select(u => new UserListDto()
        //                {
        //                    Id = u.Id,
        //                    UserName = u.UserName,
        //                    FullName = u.FullName,
        //                    Email = u.Email,
        //                    PhoneNumber = u.PhoneNumber
        //                })
        //                .ToListAsync();
        //}

        //// GET: api/Users/5
        //[ResponseType(typeof(UserDto))]
        //public IActionResult GetUserById(string id)
        //{
        //    var userInDb = db.Users
        //        .Select(u => new UserDto()
        //        {
        //            Id = u.Id,
        //            UserName = u.UserName,
        //            FullName = u.FullName,
        //            Email = u.Email,
        //            PhoneNumber = u.PhoneNumber
        //        })
        //        .FirstOrDefault(u => u.Id == id);
        //    if (userInDb == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(userInDb);
        //}

        //// PUT: api/users/updateUser/5
        //[ResponseType(typeof(void))]
        //[Route("api/users/updateUser/{id}")]
        //[HttpPut]
        //public async Task<IActionResult> UpdateUser(string id, UserDto user)
        //{
        //    if (id != user.Id)
        //    {
        //        return BadRequest();
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var userInDb = db.Users.Find(id);

        //    if (!string.IsNullOrEmpty(user.FullName)) userInDb.FullName = user.FullName;
        //    if (!string.IsNullOrEmpty(user.Email)) userInDb.Email = user.Email;
        //    if (!string.IsNullOrEmpty(user.Email)) userInDb.PhoneNumber = user.PhoneNumber;
        //    db.Entry(userInDb).State = EntityState.Modified;
        //    try
        //    {
        //        await db.SaveChangesAsync();
        //        var roles = await _manager.GetRolesAsync(userInDb.Id);
        //        if (roles.Count > 0)
        //        {
        //            await _manager.RemoveFromRolesAsync(userInDb.Id, roles.ToArray());
        //        }
        //        if (user.Roles != null && user.Roles.Length > 0)
        //        {
        //            await _manager.AddToRolesAsync(userInDb.Id, user.Roles.ToArray());
        //        }
        //        else
        //        {
        //            await _manager.AddToRoleAsync(user.Id, "Registerer");
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError();
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: /Account/ResetPassword/5
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

        //    var adminUser = await _manager.FindByNameAsync(User.Identity.Name);
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
        //    var user = await _manager.FindByIdAsync(id);
        //    if (user == null)
        //    {
        //        return BadRequest();
        //    }
        //    user.PasswordHash = new PasswordHasher().HashPassword(model.Password ?? "password");

        //    try
        //    {
        //        await _manager.UpdateAsync(user);
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        throw new DbUpdateConcurrencyException(ex.Message);
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}


        //// DELETE: api/Users/DeleteUser/5
        //[ResponseType(typeof(string))]
        //public IActionResult DeleteUser(string id)
        //{
        //    var user = db.Users.Find(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Users.Remove(user);
        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        throw new DbUpdateConcurrencyException(ex.Message);
        //    }

        //    return Ok(user.Id);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool AppUserExists(string id)
        //{
        //    return db.Users.Count(e => e.Id == id) > 0;
        //}
    }
}