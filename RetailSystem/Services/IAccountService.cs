using RetailSystem.Dtos;
using RetailSystem.Models;
using System.Threading.Tasks;

namespace RetailSystem.Services
{
    public interface IAccountService
    {
        Task<AppUser> AuthenticateAsync(AuthDto auth);
        void ChangePassword(AppUser user, string newPassword);
        bool VerifyPasswordHash(string password, string userHashString, string userSaltString);
    }
}
