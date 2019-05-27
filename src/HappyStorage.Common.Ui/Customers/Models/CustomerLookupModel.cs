using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HappyStorage.Common.Ui.Customers
{
    public class CustomerLookupModel
    {
        [Display(Name = "Customer Number")]
        public string CustomerNumber { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }
    }
}
