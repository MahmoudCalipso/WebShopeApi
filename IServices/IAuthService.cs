using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopeApi.ModelRequest;
using WebShopeApi.Models;

namespace WebShopeApi.IServices
{
    public interface IAuthService
    {
        Task<ActionResult<User>> SignIn(LoginRequest request);
        Task<User> GetUser(string Email, string Password);
        bool IsAnExistingUser(string Email);
        Task<bool> IsValidUserCredentials(string Email, string Password);
        Task<ActionResult<User>> SignUp(User user);
    }
}
