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
        Task<IEnumerable<AppUser>> GetAllAsync();
        Task<AppUser> GetByIdAsync(int id);
        AppUser Create(RegisterDto user);
        void Update(AppUser user);
        void Delete(AppUser user);
    }
}
