using HappyStorage.Common.Ui.Units.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HappyStorage.Ui.Pages.Unit
{
    public class ListModel : PageModel
    {
        public ListModel(IUnitListViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public IUnitListViewModel ViewModel { get; private set; }

        public IActionResult OnGet()
        {
            ViewModel.Load();
            return Page();
        }


        [BindProperty(Name = "ViewModel.Filter.IsClimateControlled")]
        public bool? ClimateControlled { get; set; }

        [BindProperty(Name = "ViewModel.Filter.IsVehicleAccessible")]
        public bool? VehicleAccessible { get; set; }

        [BindProperty(Name = "ViewModel.Filter.MinimumCubicFeet")]
        public int? MinimumCubicFeet { get; set; }

        public IActionResult OnPost()
        {
            ViewModel.ApplyFilter(VehicleAccessible, ClimateControlled, MinimumCubicFeet);
            return Page();
        }
    }
}