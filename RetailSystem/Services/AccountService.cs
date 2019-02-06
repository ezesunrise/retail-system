using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RetailSystem.Data;
using RetailSystem.Dtos;
using RetailSystem.Helpers;
using RetailSystem.Models;

namespace RetailSystem.Services
{
    public class AccountService : IAccountService
    {
        private ApplicationDbContext _context;
        private AppSettings _appSettings;
        private readonly IMapper _mapper;
        
        public AccountService(ApplicationDbContext context, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }
        
        public async Task<AppUser> AuthenticateAsync(AuthDto auth)
        {
            if (string.IsNullOrEmpty(auth.UserName) || string.IsNullOrWhiteSpace(auth.Password))
            {
                return null;
            }
            
            var user = await _context.AppUsers.SingleOrDefaultAsync(u => u.UserName == auth.UserName);
            if (user == null)
            {
                return null;
            }
            //check password validity
            if (!VerifyPasswordHash(auth.Password, user.PasswordHash, user.PasswordSalt)) {
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

        public void ChangePassword(AppUser user, string newPassword)
        {
            string hash;
            string salt;
            CreatePasswordHash(newPassword, out hash, out salt);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;

            _context.Entry(user).State = EntityState.Modified;
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

        public bool VerifyPasswordHash(string password, string userHashString, string userSaltString)
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
