using System.ComponentModel.DataAnnotations;

namespace HappyStorage.Common.Ui.Customers.Models
{
    public class CustomerLookupModel
    {
        [Display(Name = "Customer Number")]
        public string CustomerNumber { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }
    }
}