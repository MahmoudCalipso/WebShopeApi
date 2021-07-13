using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShopeApi.ModelRequest
{
    public class LoginResult
    {
        public string Email { get; internal set; }
        public long UserId { get; internal set; }
        public string Username { get; internal set; }
        public string AccessToken { get; internal set; }
        public string TypeUser { get; internal set; }
        public string RefreshToken { get; internal set; }
    }
}
