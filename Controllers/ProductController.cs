using Microsoft.AspNetCore.Authorization;
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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: api/<ProductController>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _productService.GetProducts();
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Product>> GetProduct(long id)
        {
            return await _productService.GetProduct(id);
        }

        // POST api/<ProductController>
        [HttpPost]
        [Authorize(Roles = TypeUser.Admin)]
        public async Task<ActionResult<Product>> PostProduct(Product prod)
        {
            return await _productService.PostProduct(prod);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = TypeUser.Admin)]
        public async Task<ActionResult<Product>> PutProduct(long id, Product product)
        {
           if(id != product.ProductId)
            {
                return BadRequest();
            }

            return await _productService.PutProduct(id, product);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = TypeUser.Admin)]
        public async Task<ActionResult<Product>> DeleteProduct(long id)
        {
            return await _productService.DeleteProduct(id);
        }
    }
}
