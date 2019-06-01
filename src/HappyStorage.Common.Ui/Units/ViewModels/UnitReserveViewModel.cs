using System;
using System.Collections.Generic;
using System.Linq;
using HappyStorage.Core;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        private List<SelectListItem> FetchCustomers()
        {
            return facade.ListCustomers().Select(c =>
                new SelectListItem()
                {
                    Text = c.FullName,
                    Value = c.CustomerNumber
                }).ToList();
        }

        public string SelectedCustomerNumber { get; set; }

        public void ReserveUnit(string unitNumber, string customerNumber)
        {
            facade.ReserveUnit(unitNumber, customerNumber);
        }
    }
}
