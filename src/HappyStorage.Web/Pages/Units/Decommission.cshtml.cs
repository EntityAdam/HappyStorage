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

        public void OnGet([FromRoute] string unitNumber)
        {
            ViewModel.UnitNumber = unitNumber;
        }

        public IActionResult OnPost([FromForm(Name = "ViewModel.UnitNumber")] string unitNumber)
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