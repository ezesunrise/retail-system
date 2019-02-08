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
        Task<IEnumerable<AppUser>> GetAsync(int businessId, int locationId);
        Task<AppUser> GetByIdAsync(int id);
        Task<AppUser> GetByUserNameAsync(string userName);
        AppUser Create(RegisterDto user);
        void Update(AppUser user);
        void Delete(AppUser user);
        void ResetPassword(AppUser user);
    }
}
