using HappyStorage.Common.Ui.Units.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HappyStorage.Web.Pages.Unit
{
    public class DeleteModel : PageModel
    {
        public DeleteModel(IUnitDecommissionViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public IUnitDecommissionViewModel ViewModel { get; private set; }

        public string UnitNumber { get; set; }

        // => /unit/delete/unit-01
        public void OnGet([FromRoute] string unitNumber)
        {
            UnitNumber = unitNumber;
        }

        public IActionResult OnPost([FromForm] string unitNumber)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            ViewModel.Decommission(unitNumber);
            return RedirectToPage("List");
        }
    }
}