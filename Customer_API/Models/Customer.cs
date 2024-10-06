using System.Globalization;

namespace Customer_API.Models
{
    public class Customer
    {
        public int customer_id { get; set; }

        public string customer_name { get; set; }

        public string address { get; set; }

        public string city { get; set; }

        public string state { get; set; }

        public string zip_code { get; set; }

    }
}
