using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebShopeApi.Models
{
    public class Sales
    {
        [Key]
        public long SalesId { get; set; }

        [Column]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage ="The CreationDate field is invalid.")]
        public DateTime CreatedSales { get; set; }

        [Column]
        public double TotalPriceSales { get; set; }
        [Column]
        public int QteProductSales { get; set; }


        [Column]
        [Required]
        public long UserId { get; set; }
        public User User { get; set; }
        public virtual IQueryable<SalesProd> SalesProds { get; set; }


    }
}
