using HappyStorage.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace HappyStorage.Common.Ui.Units.ViewModels
{
    public class UnitReserveViewModel : IUnitReserveViewModel
    {
        private readonly IFacade facade;

        public UnitReserveViewModel(IFacade facade)
        {
            this.facade = facade;
        }

        public string UnitNumber { get; set; }

        public List<SelectListItem> Customers => FetchCustomers();

        public string SelectedCustomerNumber { get; set; }

        public List<SelectListItem> FetchCustomers()
        {
            return facade.ListCustomersAndTenants().Select(c =>
                new SelectListItem()
                {
                    Text = c.FullName,
                    Value = c.CustomerNumber
                }).ToList();
        }

        public void ReserveUnit()
        {
            facade.ReserveUnit(this.UnitNumber, this.SelectedCustomerNumber);
        }

        public void ReserveUnit(string unitNumber, string customerNumber)
        {
            facade.ReserveUnit(unitNumber, customerNumber);
        }
    }
}