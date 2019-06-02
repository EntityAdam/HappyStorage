﻿using HappyStorage.Common.Ui.Tenants.Models;
using HappyStorage.Core;
using System.Collections.Generic;
using System.Linq;

namespace HappyStorage.Common.Ui.Tenants.ViewModels
{
    //TODO: Default SelectedCustomerNumber value.
    public class TenantUnitsViewModel : ITenantUnitsViewModel
    {
        private readonly IFacade facade;

        public string SelectedCustomerNumber { get; set; }

        public List<TenantUnitModel> TenantUnits => GetTenantUnits();

        public TenantUnitsViewModel(IFacade facade)
        {
            this.facade = facade;
        }
        private List<TenantUnitModel> GetTenantUnits()
        {
            return GetTenantUnits(this.SelectedCustomerNumber);
        }
        private List<TenantUnitModel> GetTenantUnits(string customerNumber)
        {
            return facade.GetCustomerUnits(customerNumber)
                .Select(c =>
                new TenantUnitModel()
                {
                    UnitNumber = c.unitNumber,
                    ReservationDate = c.reservationDate,
                    AmountPaid = c.amountPaid
                }).ToList();
        }
    }
}