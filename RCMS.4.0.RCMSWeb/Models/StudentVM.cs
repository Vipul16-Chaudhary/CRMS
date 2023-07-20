
using System.ComponentModel.DataAnnotations;

namespace RCMS._4._0.RCMSWeb.Models
{
    public class StudentVM
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Enter your name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter your address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Enter your City")]
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        [Required(ErrorMessage = "Your must provide a Mobile Number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Mobile number")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Enter Your Mail ID")]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
    }
}
