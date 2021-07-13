using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebShopeApi.Models
{
    public class Product
    {
        [Key]
        public long ProductId { get; set; }

        [Column]
        [Required(ErrorMessage = "The ProductName field is required.") ]
        [MinLength(3)]
        [MaxLength(20)]
        public string ProductName { get; set;  }

        [Column]
        [Required(ErrorMessage = "The ProductType field is required.")]
        [MinLength(3)]
        [MaxLength(20)]
        public string ProductType { get; set; }

        [Column]
        [Required(ErrorMessage = "The ProductQte field is required.")]
        public int ProductQte { get; set; }

        [Column]
        [Required(ErrorMessage = "The ProductPrice field is required.")]
        public double ProductPrice { get; set; }
    }
}
