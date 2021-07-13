using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopeApi.IServices;
using WebShopeApi.Models;

namespace WebShopeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController (IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Authorize(Roles = TypeUser.Admin)]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _userService.GetUsers();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id) 
        {
            return await _userService.GetUser(id);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> PutUser(long id, User user)
        { 
            if(id != user.UserId)
            {
                return BadRequest();
            }
            return await _userService.PutUser(id, user);
        }
        [HttpPost]
        
        public async Task<ActionResult<User>> PostUser(User user) 
        {
            return await _userService.PostUser(user);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(long id) 
        {
            return await _userService.DeleteUser(id);
        }
    }
}
