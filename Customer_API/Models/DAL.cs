using System.Data;
using System.Data.SqlClient;

namespace Customer_API.Models
{
    public class DAL
    {
        public Response GetAllCustomer(SqlConnection connection)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("Select * from customers", connection);
            DataTable dt = new DataTable();
            List<Customer> lstCustomer = new List<Customer>();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Customer customer = new Customer();
                    customer.customer_id = Convert.ToInt32(dt.Rows[i]["customer_id"]);
                    customer.customer_name = Convert.ToString(dt.Rows[i]["customer_name"]);
                    customer.address = Convert.ToString(dt.Rows[i]["address"]);
                    customer.city = Convert.ToString(dt.Rows[i]["city"]);
                    customer.state = Convert.ToString(dt.Rows[i]["state"]);
                    customer.zip_code = Convert.ToString(dt.Rows[i]["zip_code"]);
                    lstCustomer.Add(customer);
                }
            }
            if (lstCustomer.Count > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Data Found";
                response.ListCustomer = lstCustomer;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Data Found";
                response.ListCustomer = null;
            }

            return response;
        }

        public Response GetCustomerById(SqlConnection connection, int id)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("Select * from customers where customer_id='" + id + "'", connection);
            DataTable dt = new DataTable();

            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Customer customer = new Customer();
                customer.customer_id = Convert.ToInt32(dt.Rows[0]["customer_id"]);
                customer.customer_name = Convert.ToString(dt.Rows[0]["customer_name"]);
                customer.address = Convert.ToString(dt.Rows[0]["address"]);
                customer.city = Convert.ToString(dt.Rows[0]["city"]);
                customer.state = Convert.ToString(dt.Rows[0]["state"]);
                customer.zip_code = Convert.ToString(dt.Rows[0]["zip_code"]);

                response.StatusCode = 200;
                response.StatusMessage = "Data Found";
                response.Customer = customer;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Data Found";
                response.Customer = null;
            }

            return response;
        }

        public Response AddCustomer(SqlConnection connection, Customer customer)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("INSERT INTO customers(customer_name,address,city,state,zip_code) VALUES('"+customer.customer_name+"','"+customer.address+"','"+customer.city+"','"+customer.state+"','"+customer.zip_code+"')", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            

            if (i > 0)
            { 
                response.StatusCode = 200;
                response.StatusMessage = "Customer Added";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Data Inserted";
                response.Customer = null;
            }

            return response;
        }

        public Response UpdateCustomer(SqlConnection connection, Customer customer)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("update customers set customer_name = '" + customer.customer_name + "',address = '" + customer.address + "',city = '" + customer.city + "',state = '" + customer.state + "',zip_code = '" + customer.zip_code + "' where customer_id = '"+customer.customer_id+"'", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();


            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Customer Updated";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No Data updated";
                response.Customer = null;
            }

            return response;
        }

        public Response DeleteCustomer(SqlConnection connection, int id)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("delete from customers where customer_id = '" + id + "'", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();


            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Customer Deleted";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Customer Deletion failed";
                response.Customer = null;
            }

            return response;
        }
    }
}
