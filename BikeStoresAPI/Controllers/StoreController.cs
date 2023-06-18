using BikeStoresAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
        public IEnumerable<Store> GetStores() {
            List<Store> lstStores = new List<Store>();
            Utility u = new Utility();
            DataTable dtStores = u.SqlQueryToDataTable(@"SELECT store_id, store_name, phone, email, street, city, state, zip_code FROM sales.stores");
            foreach (DataRow dr in dtStores.Rows)
            {
                Store store = new Store {
                    Id = Convert.ToInt32(dr["store_id"]),
                    Name = dr["store_name"].ToString(),
                    Phone = dr["phone"].ToString(),
                    Email = dr["email"].ToString(),
                    Street = dr["street"].ToString(),
                    City = dr["city"].ToString(),
                    State = dr["state"].ToString(),
                    ZipCode = dr["zip_code"].ToString()
                };
                lstStores.Add(store);
            }
            return lstStores;
        }

        /// <summary>
        /// Get store by ID
        /// </summary>
        /// <param name="id">store_id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public IEnumerable<Store> GetStore([FromRoute] int id) {
            List<Store> lstStores = new List<Store>();
            Utility u = new Utility();
            DataTable dtStores = u.SqlQueryToDataTable(@"SELECT store_id, store_name, phone, email, street, city, state, zip_code FROM sales.stores WHERE store_id = " + id.ToString());
            foreach (DataRow dr in dtStores.Rows)
            {
                Store store = new Store
                {
                    Id = Convert.ToInt32(dr["store_id"]),
                    Name = dr["store_name"].ToString(),
                    Phone = dr["phone"].ToString(),
                    Email = dr["email"].ToString(),
                    Street = dr["street"].ToString(),
                    City = dr["city"].ToString(),
                    State = dr["state"].ToString(),
                    ZipCode = dr["zip_code"].ToString()
                };
                lstStores.Add(store);
            }
            return lstStores;
        }

        /// <summary>
        /// Create a new store
        /// </summary>
        /// <param name="store_name">store_name</param>
        /// <param name="phone">phone</param>
        /// <param name="email">email</param>
        /// <param name="street">street</param>
        /// <param name="city">city</param>
        /// <param name="state">state</param>
        /// <param name="zip_code">zip_code</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InsertStore(string store_name, string phone, string email, string street, string city, string state, string zip_code) {
            Utility u = new Utility();
            bool isSucceeded = u.SqlNonQuery(@"INSERT INTO sales.stores (store_name, phone, email, street, city, state, zip_code)
VALUES ('" + store_name + "', '" + phone + "', '" + email + "', '" + street + "', '" + city + "', '" + state + "', '" + zip_code + "')");
            return isSucceeded ? Ok() : BadRequest();
        }

        /// <summary>
        /// Delete a store by ID
        /// </summary>
        /// <param name="id">store_id</param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult DeleteStore(int id) {
            Utility u = new Utility();
            bool isSucceeded = u.SqlNonQuery(@"DELETE FROM sales.stores WHERE store_id = " + id.ToString());
            return isSucceeded ? Ok() : BadRequest();
        }
    }
}
