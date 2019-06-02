using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HappyStorage.Common.Ui.Tenants.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HappyStorage.Web.Pages.Tenants
{
    public class TenantUnitsModel : PageModel
    {
        public ITenantUnitsViewModel ViewModel;

        public TenantUnitsModel(ITenantUnitsViewModel viewModel)
        {
            this.ViewModel = viewModel;
        }

        public void OnGet(string customerNumber)
        {
            ViewModel.SelectedCustomerNumber = customerNumber;
        }
    }
}