using BikeStoresAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection.Metadata;
using System.Text;
using System.Web.Http.Cors;

namespace BikeStoresAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoreController : ControllerBase
    {
        public BikeStoresContext context = new BikeStoresContext();

        /// <summary>
        /// Get store list
        /// </summary>
        /// <returns></returns>
        [HttpGet("All")]
        public IActionResult GetStores() {
            try
            {
                var stores = context.Stores.Select(s => new { s.StoreId, s.StoreName, s.Email, s.Phone, s.Street, s.City, s.State, s.ZipCode });
                return Ok(stores);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Get store by ID
        /// </summary>
        /// <param name="id">storeId</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetStore([FromRoute] int id) {
            try
            {
                var store = context.Stores.Select(s => new { s.StoreId, s.StoreName, s.Email, s.Phone, s.Street, s.City, s.State, s.ZipCode }).Single(s => s.StoreId == id);
                return Ok(store);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("CreateStore")]
        public async Task<ActionResult> CreateStore()
        {
            try
            {
                var csv = HttpContext.Request.Form.Files[0];
                Stream s = csv.OpenReadStream();
                using (StreamReader reader = new StreamReader(s, Encoding.UTF8))
                {
                    List<string> lst = reader.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
                    foreach (string row in lst.Skip(1).Where(w => w.Trim() != string.Empty))
                    {
                        List<string> col = row.Split(',', StringSplitOptions.None).ToList();
                        var store = new Store { StoreName = col[0], Email = col[1], Phone = col[2], Street = col[3], City = col[4], State = col[5], ZipCode = col[6] };
                        context.Stores.Add(store);
                        await context.SaveChangesAsync();
                    }
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Update a store
        /// </summary>
        /// <param name="id">storeId</param>
        /// <param name="store_after">Store object</param>
        /// <returns></returns>
        [HttpPut("UpdateName")]
        public async Task<ActionResult> UpdateStore(int id, string name_after) {
            try
            {
                Store store_before = context.Stores.Single(s => s.StoreId == id);
                store_before.StoreName = !string.IsNullOrEmpty(name_after) ? name_after : store_before.StoreName;
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Delete a store by ID
        /// </summary>
        /// <param name="id">storeId</param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteStore(int id) {
            try
            {
                Store store = context.Stores.Single(s => s.StoreId == id);
                context.Remove(store);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Create new stores
        /// </summary>
        /// <param name="stores">Array of stores</param>
        /// <returns></returns>
        //[HttpPost("Create")]
        //public IActionResult CreateStore([FromBody] List<Store> stores) {
        //    try
        //    {
        //        foreach (Store store in stores)
        //        {
        //            context.Stores.Add(store);
        //        }
        //        context.SaveChanges();
        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}
    }
}
