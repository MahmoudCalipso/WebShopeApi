using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopeApi.Models;

namespace WebShopeApi.IServices
{
    public interface IProductService
    {
        Task<ActionResult<IEnumerable<Product>>> GetProducts();
        Task<ActionResult<Product>> GetProduct(long id);
        Task<ActionResult<Product>> PutProduct(long id, Product prod);
        Task<ActionResult<Product>> PostProduct(Product prod);
        Task<ActionResult<Product>> DeleteProduct(long id);
    }
}
