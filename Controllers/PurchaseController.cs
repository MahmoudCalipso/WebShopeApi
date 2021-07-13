using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class PurchaseController : ControllerBase
    {
        private readonly ISalesService _salesService;
        public PurchaseController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sales>>> GetSales() 
        {
            return await _salesService.GetSales();
        }
        [HttpGet("PurchaseById/{id}")]
        public async Task<ActionResult<IEnumerable<Sales>>> GetPurchaseById(long id)
        {
            return await _salesService.GetPurchaseById(id);
        }
        [HttpGet("SalesForCutomer/{id}")]
        public async Task<ActionResult<IEnumerable<Sales>>> GetSalesForCutomer(long iduser)
        {
            return await _salesService.GetSalesForCutomer(iduser);
        }
        [HttpGet("user_ref/{iduser}/purchase/{idsale}")]
        public async Task<ActionResult<Sales>> GetOnePurchaseForCustomer(long iduser, long idsale)
        {
            return await _salesService.GetOnePurchaseForCustomer(iduser, idsale);
        }
       

        [HttpPut("{id}")]
        public async Task<ActionResult<SalesProd>> PutProdSales(long id, SalesProd prodSales)
        {
            return await _salesService.PutProdSales(id, prodSales);
        }
        [HttpPost("PostSales")]
        public async Task<ActionResult<Sales>> PostSales(Sales sales)
        {
            return await _salesService.PostSales(sales);
        }
        [HttpPost("PostProdSales")]
        public async Task<ActionResult<SalesProd>> PostListSalesProds(SalesProd SalesProd)
        {
            return await _salesService.PostListSalesProds(SalesProd);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Sales>> DeleteSeles(long id)
        {
            return await _salesService.DeleteSeles(id);

        }
    }
}
