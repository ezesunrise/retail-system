using RetailSystem.Dtos;
using RetailSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailSystem.Services
{
    public interface IAppUserService
    {
        AppUser Authenticate(string userName, string password);
        IEnumerable<AppUser> GetAll();
        AppUser GetById(string id);
        AppUser Create(AppUserDto user, string password);
        AppUser Update(AppUserDto user, string password = null);
        void Delete(string id);
    }
}
