using HappyStorage.Common.Ui.Units.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HappyStorage.Ui.Pages.Unit
{
    public class ListModel : PageModel
    {
        public ListModel(IUnitListViewModel viewModel)
        {
            this.ViewModel = viewModel;
        }

        public IUnitListViewModel ViewModel { get; private set; }

        public IActionResult OnGet()
        {
            ViewModel.Load();
            return Page();
        }

        public IActionResult OnPost([FromForm] bool? isVehicleAccessible, [FromForm] bool? isClimateControlled, [FromForm] int? minimumCubicFeet)
        {
            ViewModel.ApplyFilter(isVehicleAccessible, isClimateControlled, minimumCubicFeet);
            return Page();
        }
    }
}