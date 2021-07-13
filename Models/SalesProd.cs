using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebShopeApi.Models
{
    public class SalesProd
    {
        [Key]
        public long SalesProdId { get; set; }

        [Column]
        [Required]
        public long ProductId { get; set; }
        public Product Product { get; set; }

        [Column]
        [Required(ErrorMessage ="The QteProd field is invalid") ]
        public int QteProd {get;set;}


        [Column]
        [Required]
        public long SalesId { get; set; }
        public Sales Sales { get; set; }



    }
}
