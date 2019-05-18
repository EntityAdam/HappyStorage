using HappyStorage.Ui.Common.Models;
using HappyStorage.Ui.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HappyStorage.Ui.Pages.Unit
{

    public class CreateModel : PageModel
    {
        private readonly IUnitCommissionViewModel _viewModel;

        public CreateModel(IUnitViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost([FromForm] UnitModel newUnit)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _viewModel.Create(newUnit);

            return RedirectToPage("List");
        }
    }
}