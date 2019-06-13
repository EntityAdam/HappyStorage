using HappyStorage.Common.Ui.Customers.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HappyStorage.Web.Pages.Customer
{
    public class ListModel : PageModel
    {
        public ListModel(ICustomerListViewModel viewModel)
        {
            ViewModel = viewModel;
            ViewModel.Load();
        }

        public ICustomerListViewModel ViewModel { get; private set; }

        public IActionResult OnGet([FromQuery] int? pageNum)
        {
            if (pageNum != null)
            {
                ViewModel.JumpToPage(pageNum);
            }
            return Page();
        }
    }
}