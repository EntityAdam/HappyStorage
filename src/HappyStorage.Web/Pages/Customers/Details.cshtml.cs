using HappyStorage.Common.Ui.Customers.Models;
using HappyStorage.Common.Ui.Customers.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HappyStorage.Web.Pages.Customers
{
    public class DetailsModel : PageModel
    {
        public DetailsModel(ICustomerDetailsViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public ICustomerDetailsViewModel ViewModel { get; private set; }

        public IActionResult OnGet([FromRoute] string customerNumber)
        {
            ViewModel.GetCustomer(customerNumber);

            if (ViewModel.NewCustomer == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost([FromForm(Name = "ViewModel.NewCustomer")] NewCustomerModel newCustomer)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ViewModel.Update(newCustomer);
            return RedirectToPage("List");
        }
    }
}