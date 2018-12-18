using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RetailSystem.Data;
using RetailSystem.Dtos;
using RetailSystem.Models;

namespace RetailSystem.Services
{
    public class AppUserService //: IAppUserService
    {
        private ApplicationDbContext _context;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        UserStore<AppUser> _userStore;
        UserManager<AppUser> _manager;

        //public AppUserService(ApplicationDbContext context, IMapper mapper)
        //{
        //    _context = context;
        //    _userStore = new UserStore<AppUser>(_context);
        //    //_manager = new UserManager<AppUser>(_userStore);
        //    _mapper = mapper;
        //}

        //AppUser IAppUserService.Authenticate(string userName, string password)
        //{
        //    if(string.IsNullOrEmpty(userName) || string.IsNullOrWhiteSpace(password))
        //    {
        //        return null;
        //    }
        //    var user = _userStore..Users.SingleOrDefault(u => u.UserName == userName);
        //    if(user == null)
        //    {
        //        return null;
        //    }
        //    //check password validity
        //    if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) { }

        //    return user;
        //}

        //AppUser IAppUserService.Create(AppUserDto user, string password)
        //{
        //    throw new NotImplementedException();
        //}

        //void IAppUserService.Delete(string id)
        //{
        //    var user = _userStore.Users.SingleOrDefault(u => u.Id == id);
        //    if(user != null)
        //    {
        //        _userStore.DeleteAsync(user);
        //    }
        //}

        //IEnumerable<AppUser> IAppUserService.GetAll()
        //{
        //    return _userStore.Users.ToList();
        //}

        //AppUser IAppUserService.GetById(string id)
        //{
        //    return _userStore.Users.SingleOrDefault(u => u.Id == id);
        //}

        //AppUser IAppUserService.Update(AppUserDto user, string password)
        //{
        //    throw new NotImplementedException();
        //}

        //private static void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
        //{
        //    if (password == null) throw new ArgumentNullException("password");
        //    if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace");

        //    using (var hmac = new System.Security.Cryptography.HMACSHA512())
        //    {
        //        passwordSalt = hmac.Key.ToString();
        //        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)).ToString();
        //    }
        //}
        //private static bool VerifyPasswordHash(string password, string userHash, string userSalt)
        //{
        //    if (password == null) throw new ArgumentNullException("password");
        //    if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace");
        //    if (userHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected)", "passwordHash");
        //    if (userSalt.Length != 128) throw new ArgumentException("Invalid length of password hash (128 bytes expected)", "passwordSalt");

        //    using (var hmac = new System.Security.Cryptography.HMACSHA512(userSalt))
        //    {
        //        var computedHash = hmac.ComputeHash(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
        //        for(int i = 0; i < computedHash.Length; i++)
        //        {
        //            if (computedHash[i] != userHash[i]) return false;
        //        }

        //        return true;
        //    }
        //}
    }
}
