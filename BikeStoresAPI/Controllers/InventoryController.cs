using BikeStoresAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BikeStoresAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        BikeStoresContext context = new BikeStoresContext();

        /// <summary>
        /// Get inventory quantity of all products from all stores
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetInventory()
        {
            try
            {
                var joined = from t in context.Stocks.ToList()
                                join s in context.Stores.ToList() on t.StoreId equals s.StoreId
                                join p in context.Products.ToList() on t.ProductId equals p.ProductId
                                join b in context.Brands.ToList() on p.BrandId equals b.BrandId
                                join c in context.Categories.ToList() on p.CategoryId equals c.CategoryId
                                select new { p.ProductId, p.ProductName, b.BrandName, c.CategoryName, s.StoreId, s.StoreName, t.Quantity };
                return Ok(joined);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);

            }
        }

        /// <summary>
        /// Get inventory of a product by product ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("product/{id}")]
        public IActionResult GetProductInventory(int id) {
                try
                {
                    var filtered = from t in context.Stocks.ToList()
                                   join s in context.Stores.ToList() on t.StoreId equals s.StoreId
                                   join p in context.Products.ToList() on t.ProductId equals p.ProductId where p.ProductId == id
                                   join b in context.Brands.ToList() on p.BrandId equals b.BrandId
                                   join c in context.Categories.ToList() on p.CategoryId equals c.CategoryId
                                   select new { p.ProductId, p.ProductName, b.BrandName, c.CategoryName, s.StoreId, s.StoreName, t.Quantity };
                    return Ok(filtered);
                }
                catch (Exception e)
                {
                    return NotFound(e.Message);
                }
        }

        /// <summary>
        /// Get inventory
        /// </summary>
        /// <returns></returns>
//        [HttpGet]
//        [Route("s")]
//        public IActionResult GetInventory() {
//            Utility u = new Utility();
//            string strQuery = @"SELECT
//	p.product_id,
//	p.product_name,
//	b.brand_name,
//	c.category_name,
//	s.store_id,
//	s.store_name,
//	t.quantity
//FROM production.stocks t
//JOIN sales.stores s on t.store_id = s.store_id
//JOIN production.products p ON t.product_id = p.product_id
//JOIN production.brands b ON p.brand_id = b.brand_id
//JOIN production.categories c ON p.category_id = c.category_id";
//            string response = u.SqlQueryToString(strQuery);
//            return Ok(response);
//        }
    }
}
