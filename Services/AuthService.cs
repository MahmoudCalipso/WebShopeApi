using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopeApi.IServices;
using WebShopeApi.ModelRequest;
using WebShopeApi.Models;
using WebShopeApi.Settings;

namespace WebShopeApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly DbShopContext _context;
        public AuthService(DbShopContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser(string Email, string Password)
        {
            var auth = await _context.User.FirstOrDefaultAsync(auth => (auth.Email == Email) && 
                                                (auth.Password == Password));
            return auth;

        }

        public bool IsAnExistingUser(string Email)
        {
             var user =  _context.User.Find(Email);
            if (user != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> IsValidUserCredentials(string Email, string Password)
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                return false;
            }
            var auth = await _context.User
             .FirstOrDefaultAsync(x => (x.Email == Email) &&
                                     (x.Password == Password));

            if (auth != null)
                return true;
            else
                return false;


        }

        public async  Task<ActionResult<User>> SignIn(LoginRequest request)
        {
            var result = await  _context.User.FirstOrDefaultAsync(x => x.Email == request.Email && x.Password == request.Password);
            return result ;
        }

        public async Task<ActionResult<User>> SignUp(User user)
        {
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;

        }
    }
}
