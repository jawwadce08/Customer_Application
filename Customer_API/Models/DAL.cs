using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Customer_API.Models
{
    public class DAL
    {
        public Response GetAllCustomer(SqlConnection connection)
        {
            // Create a new Response object to hold the results
            Response response = new Response();
            // 1. Prepare the SQL command:
            SqlDataAdapter da = new SqlDataAdapter("Select * from customers", connection);
            // 2. Execute the query and fill a DataTable:
            DataTable dt = new DataTable();
            //Create a list to store Customer objects:
            List<Customer> lstCustomer = new List<Customer>();
            da.Fill(dt);

            // 3. Check if any data was retrieved:
            if (dt.Rows.Count > 0)
            {
                // 3.1 Loop through each row in the DataTable:
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Customer customer = new Customer();
                    // 3.2 Extract data from the DataRow using safe casting:
                    customer.customer_id = Convert.ToInt32(dt.Rows[i]["customer_id"]);
                    customer.customer_name = Convert.ToString(dt.Rows[i]["customer_name"]);
                    customer.address = Convert.ToString(dt.Rows[i]["address"]);
                    customer.city = Convert.ToString(dt.Rows[i]["city"]);
                    customer.state = Convert.ToString(dt.Rows[i]["state"]);
                    customer.zip_code = Convert.ToString(dt.Rows[i]["zip_code"]);
                    // 3.3 Add the Customer object to the list:
                    lstCustomer.Add(customer);
                }
            }
            if (lstCustomer.Count > 0)
            {
                // 4. Set successful response data
                response.StatusCode = 200;// HTTP status code for found
                response.StatusMessage = "Data Found";
                response.ListCustomer = lstCustomer;
            }
            else
            {
                response.StatusCode = 100; // HTTP status code for not found
                response.StatusMessage = "No Data Found";
                response.ListCustomer = null; // Set to null to avoid unnecessary data in the response
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
