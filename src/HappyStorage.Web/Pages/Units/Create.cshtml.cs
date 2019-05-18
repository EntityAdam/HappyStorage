using HappyStorage.Common.Ui.Units.Models;
using HappyStorage.Common.Ui.Units.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HappyStorage.Ui.Pages.Unit
{

    public class CreateModel : PageModel
    {
        public CreateModel(ICreateUnitViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public ICreateUnitViewModel ViewModel { get; private set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost([FromForm] NewUnitModel newUnit)
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