using BikeStoresAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BikeStoresAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        BikeStoresContext context = new BikeStoresContext();

        [HttpGet]
        public IActionResult GetProducts()
        {
            try
            {
                var joined = context.Products.ToList().Join(context.Categories.ToList(), p => p.CategoryId, c => c.CategoryId, (x, y) => new { x.ProductName, y.CategoryName });
                return Ok(joined);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
