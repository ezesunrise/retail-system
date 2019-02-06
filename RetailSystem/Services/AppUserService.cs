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
        
        public AppUser Authenticate(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }
            
            var user = _context.AppUsers.SingleOrDefault(u => u.UserName == userName);
            if (user == null)
            {
                return null;
            }
            //check password validity
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) {
                return null;
            }

            // authentication successful so generate jwt token
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            user.Token = tokenHandler.CreateEncodedJwt(tokenDescriptor);
            
            return user;
        }

        public AppUser Create(RegisterDto data)
        {
            string hash;
            string salt;
            CreatePasswordHash(data.Password, out hash, out salt);

            AppUser newUser = new AppUser() {
                UserName = data.UserName,
                Name = data.Name,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber,
                Address = data.Address,
                Photo = data.Photo,
                Status = data.Status,
                Role = data.Role,
                PasswordHash = hash,
                PasswordSalt = salt,
                NormalizedUserName = data.UserName.ToUpper(),
                NormalizedEmail = data.Email.ToUpper()
            };
            _context.AppUsers.Add(newUser);

            return newUser;
        }

        public async Task<IEnumerable<AppUser>> GetAllAsync()
        {
            return await _context.AppUsers.ToListAsync();
        }

        public async Task<AppUser> GetByIdAsync(int id)
        {
            return await _context.AppUsers.SingleOrDefaultAsync(u => u.Id == id);
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public void Delete(AppUser user)
        {
            _context.AppUsers.Remove(user);
        }

        private static void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = Convert.ToBase64String(hmac.Key);
                passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.ASCII.GetBytes(password)));
            }
        }

        private static bool VerifyPasswordHash(string password, string userHashString, string userSaltString)
        {
            var userHash = Convert.FromBase64String(userHashString);
            var userSalt = Convert.FromBase64String(userSaltString);

            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace");
            if (userHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected)", "passwordHash");
            if (userSalt.Length != 128) throw new ArgumentException("Invalid length of password hash (128 bytes expected)", "passwordSalt");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(userSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.ASCII.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != userHash[i]) return false;
                }
                return true;
            }
        }
    }
}
