using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Validate.Common
{
    public class CustomerModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Contact Number is required")]
        public string ContactNo { get; set; }
        [Remote("ValidationForWeekEnds", "Customer")]
        public string BookingDate { get; set; }
    }
}