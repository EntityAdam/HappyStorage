using HappyStorage.Ui.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HappyStorage.Web.Pages.Unit
{
    public class DeleteModel : PageModel
    {
        private readonly IDecommissionUnitViewModel _viewModel;

        public DeleteModel(IUnitViewModel viewModel)
        {
            _viewModel = viewModel;
        }

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
            _viewModel.Decommission(unitNumber);
            return RedirectToPage("List");
        }
    }
}