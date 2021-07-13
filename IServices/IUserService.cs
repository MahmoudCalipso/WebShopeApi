using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopeApi.Models;

namespace WebShopeApi.IServices
{
    public interface IUserService
    {
        Task<ActionResult<IEnumerable<User>>> GetUsers();
        Task<ActionResult<User>> GetUser(long id);
        Task<ActionResult<User>> PutUser(long id, User user);
        Task<ActionResult<User>> PostUser(User user);
        Task<ActionResult<User>> DeleteUser(long id);
    }
}
