using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopeApi.IServices;
using WebShopeApi.Models;

namespace WebShopeApi.Services
{
    public class UserService : IUserService
    {
        private readonly DbShopContext _context;
        public UserService(DbShopContext context)
        {
            _context = context;
        }
        public async Task<ActionResult<User>> DeleteUser(long id)
        {
            var user = await _context.User.FindAsync(id);
            if(user != null)
            {
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
            }
            return user;
        }

        public async Task<ActionResult<User>> GetUser(long id)
        {
            var user = await _context.User.FindAsync(id);
            return user;
        }

        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<ActionResult<User>> PostUser(User user)
        {
             await _context.User.AddAsync(user);
             await _context.SaveChangesAsync();
             return user;
        }

        public async Task<ActionResult<User>> PutUser(long id, User user)
        {
            if (id != user.UserId)
            {
                return null;
            }
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
