using CustomerApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Newtonsoft.Json;
using System.Security.Cryptography.Xml;
using System.Text;

namespace CustomerApplication.Controllers
{
    public class CustomerController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7012/api");
        private readonly HttpClient _client;

        public CustomerController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ResponseViewModel responseView = new ResponseViewModel();
            List<CustomerViewModel> customerList = new List<CustomerViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Customer/GetAllCustomer").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                responseView = JsonConvert.DeserializeObject<ResponseViewModel>(data);
                customerList = responseView.ListCustomer;

            }
            return View(customerList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CustomerViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Customer/AddCustomer", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Customer Created.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                ResponseViewModel responseView = new ResponseViewModel();
                CustomerViewModel customer = new CustomerViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Customer/GetCustomerById/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    responseView = JsonConvert.DeserializeObject<ResponseViewModel>(data);
                    customer = responseView.Customer;
                }
                return View(customer);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            
            
        }

        [HttpPost]
        public IActionResult Edit(CustomerViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Customer/UpdateCustomer", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Customer detail updated.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            
            return View(model); 
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                ResponseViewModel responseView = new ResponseViewModel();
                CustomerViewModel customer = new CustomerViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Customer/GetCustomerById/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    responseView = JsonConvert.DeserializeObject<ResponseViewModel>(data);
                    customer = responseView.Customer;
                }
                return View(customer);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Customer/DeleteCustomer/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Customer Deleted.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }

            return View();
        }

    }
}
