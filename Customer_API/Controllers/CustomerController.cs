using Customer_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Customer_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CustomerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetAllCustomer")]
        public Response GetAllCustomer()
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("CustomerConnection").ToString());
            Response response = new Response();
            DAL dAL = new DAL();
            response = dAL.GetAllCustomer(connection);
            return response;
        }

        [HttpGet]
        [Route("GetCustomerById/{id}")]
        public Response GetCustomerById(int id)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("CustomerConnection").ToString());
            Response response = new Response();
            DAL dAL = new DAL();
            response = dAL.GetCustomerById(connection, id);
            return response;
        }

        [HttpPost]
        [Route("AddCustomer")]
        public Response AddCustomer(Customer customer)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("CustomerConnection").ToString());
            Response response = new Response();
            DAL dAL = new DAL();
            response = dAL.AddCustomer(connection, customer);
            return response;

        }

        [HttpPut]
        [Route("UpdateCustomer")]
        public Response UpdateCustomer(Customer customer)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("CustomerConnection").ToString());
            Response response = new Response();
            DAL dAL = new DAL();
            response = dAL.UpdateCustomer(connection, customer);
            return response;

        }

        [HttpDelete]
        [Route("DeleteCustomer/{id}")]
        public Response DeleteCustomer(int id)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("CustomerConnection").ToString());
            Response response = new Response();
            DAL dAL = new DAL();
            response = dAL.DeleteCustomer(connection,id);
            return response;

        }

    }
}
