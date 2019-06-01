using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace HappyStorage.Common.Ui.Units.ViewModels
{
    public interface IUnitReserveViewModel
    {
        List<SelectListItem> Customers { get; }
        List<SelectListItem> FetchCustomers();
        string UnitNumber { get; set; }
        string SelectedCustomerNumber { get; set; }
        void ReserveUnit();
        void ReserveUnit(string unitNumber, string customerNumber);
    }
}