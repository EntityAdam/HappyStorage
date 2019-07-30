using HappyStorage.Common.Ui.Units.Models;
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
            ViewModel.Load();
        }

        public IUnitListViewModel ViewModel { get; private set; }

        public IActionResult OnGet([FromQuery] int? pageNum)
        {
            if (pageNum != null)
            {
                ViewModel.JumpToPage(pageNum);
            }
            return Page();
        }

        [BindProperty(Name = "ViewModel.Filter.IsClimateControlled")]
        public bool? ClimateControlled { get; set; }

        [BindProperty(Name = "ViewModel.Filter.IsVehicleAccessible")]
        public bool? VehicleAccessible { get; set; }

        [BindProperty(Name = "ViewModel.Filter.MinimumCubicFeet")]
        public int? MinimumCubicFeet { get; set; }

        public IActionResult OnPost([FromForm(Name = "ViewModel.Filter")] UnitListFilter filter)
        {
            ViewModel.ApplyFilter(filter);
            return Page();
        }
    }
}