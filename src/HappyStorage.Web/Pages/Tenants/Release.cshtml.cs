using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HappyStorage.Common.Ui.Tenants.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HappyStorage.Web.Pages.Units
{
    public class ReleaseModel : PageModel
    {
        public ReleaseModel(ITenantReleaseViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public ITenantReleaseViewModel ViewModel { get; }

        public void OnGet(string customerNumber, string unitNumber)
        {
            ViewModel.CustomerNumber = customerNumber;
            ViewModel.UnitNumber = unitNumber;
        }

        public IActionResult OnPost(
            [FromForm(Name = "ViewModel.CustomerNumber")] string customerNumber,
            [FromForm(Name = "ViewModel.UnitNumber")] string unitNumber)
        {
            ViewModel.ReleaseUnit(unitNumber, customerNumber);

            return RedirectToPage("TenantUnits", new { customerNumber });
        }
    }
}