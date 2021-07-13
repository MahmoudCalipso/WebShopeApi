using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebShopeApi.Models
{
    public class User
    {
        [Key]
        public long UserId { get; set; }

        [Column(name: "UserName")]
        [Required (ErrorMessage ="The UserName field is invalid")]
        [MaxLength(30)]
        [MinLength(4)]
        public string UserName { get; set; }

        [Column(name: "Email")]
        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
       
        public string Email { get; set; }

        [Column(name: "Password")]
        [Required(ErrorMessage = "The Password field is required.")]
        public string Password { get; set; }

        [Column(name: "TypeUser")]
        [Required(ErrorMessage = "The TypeUser field is required.")]
        public string TypeUser { get; set; }


    }
}
