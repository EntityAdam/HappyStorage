using HappyStorage.Common.Ui.Units.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HappyStorage.Web.Pages.Unit
{
    public class ReserveModel : PageModel
    {
        public ReserveModel(IUnitReserveViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public IUnitReserveViewModel ViewModel { get; private set; }

        public void OnGet([FromRoute] string unitNumber)
        {
            ViewModel.UnitNumber = unitNumber;
        }

        public IActionResult OnPost(
            [FromForm(Name = "ViewModel.UnitNumber")] string unitNumber, 
            [FromForm(Name = "ViewModel.SelectedCustomerNumber")] string customerNumber)
        {
            ViewModel.ReserveUnit(unitNumber, customerNumber);
            return RedirectToPage("/Units/List");
        }
    }
}