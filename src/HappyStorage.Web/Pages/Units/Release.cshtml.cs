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
        public ReleaseModel(TenantReleaseViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public TenantReleaseViewModel ViewModel { get; }

        public void OnGet()
        {
        }
    }
}