using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace BikeStoresAPI.Models
{
    public class Utility
    {
        private readonly string ConnStr = "Data Source=MININT-B4F6CNK;Initial Catalog=BikeStores;User ID=sa;Password=Pa$$w0rd;Connection Timeout=15";
        public DataTable SqlQueryToDataTable(string cmdText) {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                try
                {
                    conn.Open(); //Connection Timeout starts counting, fails here when datasource error
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd); //fails here when cmd error
                    adapter.Fill(dt);
                    dt.TableName = "SuccessTable";
                }
                catch (Exception e)
                {
                    dt.TableName = e.Message;
                }
                finally {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }
            }
            return dt;
        }

        public string SqlQueryToString(string strQuery)
        {
            DataTable dt = SqlQueryToDataTable(strQuery);
            if (dt == null)
            {
                return string.Empty;
            }

            return JsonConvert.SerializeObject(dt, Formatting.Indented);
        }

        public bool SqlNonQuery(string cmdText)
        {
            bool isSucceeded;
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    isSucceeded = true;
                }
                catch (Exception)
                {
                    isSucceeded = false;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }
            }
            return isSucceeded;
        }
    }
}
