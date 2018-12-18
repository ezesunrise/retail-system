
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RetailSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataCapture.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        //public IActionResult GetRoles()
        //{
        //    var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
        //    var roleMngr = new RoleManager<IdentityRole>(roleStore);

        //    var roles = roleMngr.Roles
        //        .Select(x => new { x.Id, x.Name })
        //        .ToList();
        //    return Ok(roles);
        //}
    }
}
