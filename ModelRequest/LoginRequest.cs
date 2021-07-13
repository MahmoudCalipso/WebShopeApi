using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebShopeApi.ModelRequest
{
    public class LoginRequest
    {
        [Required]
        [JsonPropertyName("Email")]
        public string Email { get; set; }

        [Required]
        [JsonPropertyName("Password")]
        public string Password { get; set; }
    }
}
