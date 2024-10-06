namespace CustomerApplication.Models
{
    public class ResponseViewModel
    {
        public int StatusCode { get; set; }

        public string StatusMessage { get; set; }

        public CustomerViewModel Customer { get; set; }

        public List<CustomerViewModel> ListCustomer { get; set; }
    }
}
