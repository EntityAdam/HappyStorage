using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HappyStorage.Ui.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HappyStorage.Web.Pages.Unit
{
    public class ReserveModel : PageModel
    {
        private readonly IUnitViewModel _viewModel;

        public ReserveModel(IUnitViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        
        public string UnitNumber { get; private set; }

        public void OnGet([FromRoute] string unitNumber)
        {
            UnitNumber = unitNumber;
        }

        public IActionResult OnPost([FromForm] string unitNumber, [FromForm] string customerPicker)
        {
            _viewModel.ReserveUnit(unitNumber, customerPicker);
            return RedirectToPage("/Unit/List");
        }
    }
}