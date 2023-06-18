using BikeStoresAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection.Metadata;

namespace BikeStoresAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoreController : ControllerBase
    {
        /// <summary>
        /// Get store list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetStores() {
            using (BikeStoresContext context = new BikeStoresContext())
            {
                try
                {
                    List<Store> stores = context.Stores
                        .ToList();
                    return Ok(stores);
                }
                catch (Exception e)
                {
                    return NotFound(e.Message);
                }
            }
        }

        /// <summary>
        /// Get store by ID
        /// </summary>
        /// <param name="id">storeId</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetStore([FromRoute] int id) {
            using (BikeStoresContext context = new BikeStoresContext())
            {
                try
                {
                    Store store = context.Stores
                        .Single(s => s.StoreId == id);
                    return Ok(store);
                }
                catch (Exception e)
                {
                    return NotFound(e.Message);
                }
            }
        }

        /// <summary>
        /// Create a new store
        /// </summary>
        /// <param name="store">Store object</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult PostStore([FromBody] Store store) {
            using (BikeStoresContext context = new BikeStoresContext())
            {
                try
                {
                    context.Stores.Add(store);
                    context.SaveChanges();
                    return Ok(store);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }

            }
        }

        /// <summary>
        /// Update a store
        /// </summary>
        /// <param name="id">storeId</param>
        /// <param name="store_after">Store object</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateStore([FromRoute] int id, [FromBody] Store store_after) {
            using (BikeStoresContext context = new BikeStoresContext())
            {
                try
                {
                    Store store_before = context.Stores
                        .Single(s => s.StoreId == id);
                    store_before.StoreName = store_after.StoreName != null ? store_after.StoreName : store_before.StoreName;
                    store_before.Phone = store_after.Phone != null ? store_after.Phone : store_before.Phone;
                    store_before.Email = store_after.Email != null ? store_after.Email : store_before.Email;
                    store_before.Street = store_after.Street != null ? store_after.Street : store_before.Street;
                    store_before.City = store_after.City != null ? store_after.City : store_before.City;
                    store_before.State = store_after.State != null ? store_after.State : store_before.State;
                    store_before.ZipCode = store_after.ZipCode != null ? store_after.ZipCode : store_before.ZipCode;
                    context.SaveChanges();
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }

            }
        }

        /// <summary>
        /// Delete a store by ID
        /// </summary>
        /// <param name="id">storeId</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteStore([FromRoute] int id) {
            using (BikeStoresContext context = new BikeStoresContext())
            {
                try
                {
                    Store store = context.Stores
                        .Single(s => s.StoreId == id);
                    context.Remove(store);
                    context.SaveChanges();
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }

            }
        }
    }
}
