using HappyStorage.Common.Ui.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HappyStorage.Web.Pages.Customer
{
    public class CreateModel : PageModel
    {
        public CreateModel(ICustomerCreateViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public ICustomerCreateViewModel ViewModel { get; private set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        
        public IActionResult OnPost([FromForm(Name = "ViewModel.NewCustomer")] NewCustomerModel newCustomer)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ViewModel.Create(newCustomer);
            return RedirectToPage("List");
        }
    }
}
