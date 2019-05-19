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

        public string UnitNumber { get; private set; }

        public void OnGet([FromRoute] string unitNumber)
        {
            UnitNumber = unitNumber;
        }

        //TODO: Need to rework this flow
        public IActionResult OnPost([FromForm] string unitNumber, [FromForm] string customerPicker)
        {
            ViewModel.ReserveUnit(unitNumber, customerPicker);
            return RedirectToPage("/Units/List");
        }
    }
}