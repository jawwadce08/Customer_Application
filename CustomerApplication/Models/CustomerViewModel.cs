using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CustomerApplication.Models
{
    public class CustomerViewModel
    {
        [DisplayName("Customer ID")]
        public int customer_id { get; set; }
        [Required]
        [DisplayName("Customer Name")]
        public string customer_name { get; set; }
        [Required]
        [DisplayName("Address")]
        public string address { get; set; }
        [Required]
        [DisplayName("City")]
        public string city { get; set; }
        [Required]
        [DisplayName("State")]
        public string state { get; set; }
        [Required]
        [DisplayName("Zip Code")]
        public string zip_code { get; set; }
    }
}
