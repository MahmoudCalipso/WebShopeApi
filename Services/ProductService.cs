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
    public class ProductService : IProductService
    {
        private readonly DbShopContext _context;
        public ProductService(DbShopContext context)
        {
            _context = context;
        }
        public async Task<ActionResult<Product>> DeleteProduct(long id)
        {
            var prod = await _context.Product.FindAsync(id);
            if (prod != null) { 
                _context.Product.Remove(prod);
                await _context.SaveChangesAsync();
            }
            return prod;

        }

        public async Task<ActionResult<Product>> GetProduct(long id)
        {
            var prod = await _context.Product.FindAsync(id);
            return prod;
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Product.ToListAsync();
        }

        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            await _context.Product.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<ActionResult<Product>> PutProduct(long id, Product prod)
        {
            if ( id != prod.ProductId )
            {
                return null;
            }
            _context.Entry(prod).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return prod;
        }
    }
}
