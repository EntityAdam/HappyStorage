using HappyStorage.Common.Ui.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HappyStorage.Web.Pages.Customer
{
    public class ListModel : PageModel
    {
        public ListModel(ICustomerListViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public ICustomerListViewModel ViewModel { get; private set; }

        public IActionResult OnGet()
        {
            ViewModel.Load();
            return Page();
        }
    }
}