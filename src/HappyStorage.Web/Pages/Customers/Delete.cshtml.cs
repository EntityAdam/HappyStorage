using HappyStorage.Common.Ui.Customers.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HappyStorage.Web.Pages.Customers
{
    public class DeleteModel : PageModel
    {
        public DeleteModel(ICustomerDeleteViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public ICustomerDeleteViewModel ViewModel { get; private set; }

        public void OnGet([FromRoute] string customerNumber)
        {
            ViewModel.CustomerNumber = customerNumber;
        }

        public IActionResult OnPost([FromForm(Name = "ViewModel.CustomerNumber")] string customerNumber)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            ViewModel.Delete(customerNumber);
            return RedirectToPage("List");
        }
    }
}