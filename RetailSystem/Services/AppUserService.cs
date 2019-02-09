using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RetailSystem.Data;
using RetailSystem.Dtos;
using RetailSystem.Helpers;
using RetailSystem.Models;

namespace RetailSystem.Services
{
    public class AppUserService : IAppUserService
    {
        private ApplicationDbContext _context;
        private AppSettings _appSettings;
        private readonly IMapper _mapper;
        
        public AppUserService(ApplicationDbContext context, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }
        
        public AppUser Create(RegisterDto data)
        {
            string hash;
            string salt;
            CreatePasswordHash(data.Password, out hash, out salt);
            var newUser = _mapper.Map<AppUser>(data);
            newUser.PasswordHash = hash;
            newUser.PasswordSalt = salt;
            _context.AppUsers.Add(newUser);

            return newUser;
        }

        public async Task<IEnumerable<AppUser>> GetAsync(int businessId, int locationId)
        {
            if (businessId != 0)
                return await _context.AppUsers.Where(u => u.BusinessId == businessId).ToListAsync();

            else if(locationId != 0)
                return await _context.AppUsers.Where(u => u.LocationId == locationId).ToListAsync();

            return await _context.AppUsers.ToListAsync();
        }

        public async Task<AppUser> GetByIdAsync(int id)
        {
            return await _context.AppUsers.SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<AppUser> GetByUserNameAsync(string userName)
        {
            return await _context.AppUsers.SingleOrDefaultAsync(u => u.UserName == userName);
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public void Delete(AppUser user)
        {
            _context.AppUsers.Remove(user);
        }

        public void ResetPassword(AppUser user)
        {
            string hash;
            string salt;
            AppUserService.CreatePasswordHash("password", out hash, out salt);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;

            _context.Entry(user).State = EntityState.Modified;
        }

        public static void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = Convert.ToBase64String(hmac.Key);
                passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.ASCII.GetBytes(password)));
            }
        }
    }
}
