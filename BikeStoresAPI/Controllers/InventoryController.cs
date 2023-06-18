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
        /// <summary>
        /// Get inventory
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetInventory() {
            Utility u = new Utility();
            string strQuery = @"SELECT
	p.product_id,
	p.product_name,
	b.brand_name,
	c.category_name,
	s.store_id,
	s.store_name,
	t.quantity
FROM production.stocks t
JOIN sales.stores s on t.store_id = s.store_id
JOIN production.products p ON t.product_id = p.product_id
JOIN production.brands b ON p.brand_id = b.brand_id
JOIN production.categories c ON p.category_id = c.category_id";
            return u.SqlQueryToString(strQuery);
        }

        /// <summary>
        /// Get inventory by product ID and store ID
        /// </summary>
        /// <param name="product_id">product_id</param>
        /// <param name="store_id">store_id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("p/{product_id}/s/{store_id}")]
        public string GetInventoryProductStore([FromRoute] int product_id, int store_id)
        {
            Utility u = new Utility();
            string strQuery = @"SELECT
	p.product_id,
	p.product_name,
	b.brand_name,
	c.category_name,
	s.store_id,
	s.store_name,
	t.quantity
FROM production.stocks t
JOIN sales.stores s on t.store_id = s.store_id
JOIN production.products p ON t.product_id = p.product_id
JOIN production.brands b ON p.brand_id = b.brand_id
JOIN production.categories c ON p.category_id = c.category_id
WHERE p.product_id = " + product_id.ToString() + " AND s.store_id = " + store_id.ToString();
            return u.SqlQueryToString(strQuery);
        }
    }
}
