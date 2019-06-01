using HappyStorage.Common.Ui.Units.Models;
using HappyStorage.Common.Ui.Units.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HappyStorage.Ui.Pages.Unit
{

    public class CreateModel : PageModel
    {
        public CreateModel(IUnitCreateViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public IUnitCreateViewModel ViewModel { get; private set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost([FromForm(Name = "ViewModel.NewUnit")] NewUnitModel newUnit)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            ViewModel.Create(newUnit);
            return RedirectToPage("List");
        }
    }
}