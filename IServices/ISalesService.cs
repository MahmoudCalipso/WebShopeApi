using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopeApi.Models;

namespace WebShopeApi.IServices
{
    public interface ISalesService
    {
        Task<ActionResult<IEnumerable<Sales>>> GetSales();
        Task<ActionResult<IEnumerable<Sales>>> GetPurchaseById(long id);
        Task<ActionResult<IEnumerable<Sales>>> GetSalesForCutomer(long iduser);
        Task<ActionResult<Sales>> GetOnePurchaseForCustomer(long iduser, long idsale);
        Task<ActionResult<Sales>> PutSales(long id, Sales sales);
        Task<ActionResult<SalesProd>> PutProdSales(long id, SalesProd prodSales);
        Task<ActionResult<Sales>> PostSales(Sales sales);
        Task<ActionResult<SalesProd>> PostListSalesProds(SalesProd SalesProd);
        Task<ActionResult<Sales>> DeleteSeles(long id);
    }
}
