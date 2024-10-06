namespace Customer_API.Models
{
    public class Response
    {
        public int StatusCode { get; set; }

        public string StatusMessage { get; set; }

        public Customer Customer { get; set; }

        public List<Customer> ListCustomer { get; set; }
    }
}
