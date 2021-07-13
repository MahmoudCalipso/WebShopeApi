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
    public class SalesService : ISalesService
    {
        private readonly DbShopContext _Context;
        public SalesService(DbShopContext Context)
        {
            _Context = Context;
        }
        public async Task<ActionResult<Sales>> DeleteSeles(long id)
        {
            var purchase = await _Context.Sales.FindAsync(id);
            if (purchase !=null)
            {
                _Context.Sales.Remove(purchase);
                await _Context.SaveChangesAsync();
            }
            return purchase;
            
        }

        public async Task<ActionResult<Sales>> GetOnePurchaseForCustomer(long iduser, long idsale)
        {
            var purchase = await _Context.Sales.Include(x => x.User)
                                               .Include(x => x.SalesProds)
                                               .Where(p => 
                                               (p.UserId == iduser) && (p.SalesId == idsale))
                                               .FirstOrDefaultAsync();
            return purchase;

        }

        public async Task<ActionResult<IEnumerable<Sales>>> GetPurchaseById(long id)
        {
            var purchase = await _Context.Sales.Include(x => x.SalesProds)
                                               .Include(u => u.User)
                                               .Where(p=> p.SalesId == id ).ToListAsync();
            return purchase;
        }

        public async Task<ActionResult<IEnumerable<Sales>>> GetSales()
        {
            var sales = await  _Context.Sales.Include(x => x.SalesProds).ToListAsync();
            return sales;
        }

        public async Task<ActionResult<IEnumerable<Sales>>> GetSalesForCutomer(long iduser)
        {
            var sales = await _Context.Sales.Include(x => x.SalesProds).Where(u=> u.UserId == iduser).ToListAsync();
            return sales;
        }

        public async Task<ActionResult<SalesProd>> PostListSalesProds(SalesProd salesProd)
        {
            if (await QtyProductexisted(salesProd.QteProd, salesProd.ProductId)) {
                await _Context.SalesProd.AddAsync(salesProd);
                await _Context.SaveChangesAsync();
                await UpdateSalesAfterChange(salesProd.SalesId);
                return salesProd;
            }
            return null;

        }

        public async Task<ActionResult<Sales>> UpdateSalesAfterChange(long salesId)
        {
            double PriceTotal = 0;
            int QteProductSales = 0;
            var sales = await _Context.Sales.FindAsync(salesId);
            var ListSales = await _Context.SalesProd.Include(p=> p.Product).Where(x => x.SalesId == salesId).ToListAsync();
            
            foreach (SalesProd prod in ListSales)
            {
                PriceTotal += prod.Product.ProductPrice * prod.QteProd;
                QteProductSales += prod.QteProd;
                var product = await _Context.Product.FindAsync(prod.ProductId);
                var discount_Qt = product.ProductQte - prod.QteProd;
                product.ProductQte = discount_Qt;
                _Context.Entry(product).State = EntityState.Modified;
                await _Context.SaveChangesAsync();
                
            }
            if (salesId != sales.SalesId)
            {
                return null;
            }
            sales.TotalPriceSales = PriceTotal;
            sales.QteProductSales = QteProductSales;
            _Context.Entry(sales).State = EntityState.Modified;
            await _Context.SaveChangesAsync();
            return sales;
        }

        public async Task<ActionResult<Sales>> PostSales(Sales sales)
        {
            await _Context.Sales.AddAsync(sales);
            await _Context.SaveChangesAsync();
            return sales;
        }

        public async Task<ActionResult<Sales>> PutSales(long id, Sales sales)
        {
            if(id != sales.SalesId)
            {
                return null;
            }
            _Context.Entry(sales).State = EntityState.Modified;
            await _Context.SaveChangesAsync();
            return sales;
        }


        public async Task<ActionResult<SalesProd>> PutProdSales(long id, SalesProd prodSales)
        {
            if (id != prodSales.SalesProdId)
            {
                return null;
            }
            _Context.Entry(prodSales).State = EntityState.Modified;
            await _Context.SaveChangesAsync();
            return prodSales;
        }

        public async Task<bool> QtyProductexisted(int QtProd, long prodId)
        {
            var prod = await _Context.Product.FindAsync(prodId);
            if(prod.ProductQte > QtProd)
            {
                return true;
            }
            return false;
        }


    }
}
