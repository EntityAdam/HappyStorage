﻿using System;
using System.Collections.Generic;

namespace HappyStorage.Core
{
    public interface IFacade
    {
        void CommissionNewUnit(NewUnit newUnit);

        void DecommissionUnit(string unitNumber);

        void AddNewCustomer(NewCustomer newCustomer);

        void DeleteCustomer(string customerNumber);

        IEnumerable<AvailableUnit> FindAvailableUnits(bool? isClimateControlled, bool? isVehicleAccessible, int? minimumCubicFeet);

        IEnumerable<(string unitNumber, DateTime reservationDate, decimal amountPaid)> GetCustomerUnits(string customerNumber);

        void ReserveUnit(string unitNumber, string customerNumber);
        
        void UpdateCustomerDetails(NewCustomer newCustomerDetails);
        
        void ReleaseUnit(string unitNumber, string customerNumber);

        decimal CalculateAmountDue(string customerNumber);

        void Pay(string customerNumber, decimal amount);

        IEnumerable<CustomerLookup> ListCustomers();

        NewCustomer GetCustomerDetails(string customerNumber);
    }
}