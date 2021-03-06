﻿using HappyStorage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HappyStorage.Core
{
    public class Facade : IFacade
    {
        private readonly IUnitStore unitStore;
        private readonly ICustomerStore customerStore;
        private readonly ITenancyStore tenancyStore;
        private readonly IDateService dateService;

        public Facade(IUnitStore unitStore, ICustomerStore customerStore, ITenancyStore tenancyStore, IDateService dateService)
        {
            this.unitStore = unitStore ?? throw new ArgumentNullException(nameof(unitStore));
            this.customerStore = customerStore ?? throw new ArgumentNullException(nameof(customerStore));
            this.tenancyStore = tenancyStore ?? throw new ArgumentNullException(nameof(tenancyStore));
            this.dateService = dateService ?? throw new ArgumentNullException(nameof(dateService));
        }

        //TODO: This looks ugly, consider a 'cube' class?
        public void CommissionNewUnit(NewUnit newUnit)
        {
            if (newUnit == null) throw new ArgumentNullException(nameof(newUnit));
            if (String.IsNullOrWhiteSpace(newUnit.UnitNumber)) throw new ArgumentException(nameof(newUnit.UnitNumber));
            if (newUnit.Length > 0 || newUnit.Height > 0 || newUnit.Width > 0)
            {
                if (newUnit.Length == 0 || newUnit.Height == 0 || newUnit.Width == 0)
                {
                    throw new ArgumentException("The unit may not have 0 for any dimesion");
                }
                else
                {
                    checked
                    {
                        try
                        {
                            var res = newUnit.Length * newUnit.Width * newUnit.Height;
                        }
                        catch (OverflowException ex)
                        {
                            throw new ArgumentOutOfRangeException($"The unit may not exceed {int.MaxValue} cubic feet", ex);
                        }
                    }
                }
            }
            if (unitStore.UnitExists(newUnit.UnitNumber)) throw new InvalidOperationException("The unit number already exists.");
            unitStore.Create(newUnit);
        }

        public void DecommissionUnit(string unitNumber)
        {
            if (unitNumber == null) throw new ArgumentNullException(nameof(unitNumber));
            if (String.IsNullOrWhiteSpace(unitNumber)) throw new ArgumentException(nameof(unitNumber));
            if (!unitStore.UnitExists(unitNumber)) throw new InvalidOperationException("The unit number does not exist.");
            if (tenancyStore.IsUnitNumberOccupied(unitNumber)) throw new InvalidOperationException("The unit cannot be deleted because it is currently occupied.");
            unitStore.Delete(unitNumber);
        }

        public void AddNewCustomer(NewCustomer newCustomer)
        {
            if (newCustomer == null) throw new ArgumentNullException(nameof(newCustomer));
            if (String.IsNullOrWhiteSpace(newCustomer.CustomerNumber)) throw new ArgumentException(nameof(newCustomer.CustomerNumber));
            if (customerStore.CustomerExists(newCustomer.CustomerNumber)) throw new InvalidOperationException("The customer number already exists.");
            customerStore.Create(newCustomer);
        }

        public void DeleteCustomer(string customerNumber)
        {
            if (customerNumber == null) throw new ArgumentNullException(nameof(customerNumber));
            if (String.IsNullOrWhiteSpace(customerNumber)) throw new ArgumentException(nameof(customerNumber));
            if (!customerStore.CustomerExists(customerNumber)) throw new InvalidOperationException("The customer number does not exist.");
            if (tenancyStore.GetCustomerUnits(customerNumber).Any()) throw new InvalidOperationException("The customer has one or more units reserved and cannot be deleted until all units are released");
            customerStore.Delete(customerNumber);
        }

        public IEnumerable<AvailableUnit> SearchAvailableUnits(bool? isClimateControlled, bool? isVehicleAccessible, int? minimumCubicFeet)
        {
            var units = unitStore.SearchAvailableUnits(isClimateControlled, isVehicleAccessible, minimumCubicFeet);
            var occupiedUnitNumbers = tenancyStore.ListOccupiedUnits().ToArray();
            return units.Where(u => !occupiedUnitNumbers.Contains(u.UnitNumber));
        }

        public void ReserveUnit(string unitNumber, string customerNumber)
        {
            if (unitNumber == null) throw new ArgumentNullException(nameof(unitNumber));
            if (customerNumber == null) throw new ArgumentNullException(nameof(customerNumber));
            if (String.IsNullOrWhiteSpace(unitNumber)) throw new ArgumentException(nameof(unitNumber));
            if (String.IsNullOrWhiteSpace(customerNumber)) throw new ArgumentException(nameof(customerNumber));
            if (tenancyStore.IsUnitNumberOccupied(unitNumber)) throw new InvalidOperationException("The unit is already occupied.");
            tenancyStore.Create(unitNumber, customerNumber, dateService.GetCurrentDateTime(), 0m);
        }

        public void ReleaseUnit(string unitNumber, string customerNumber)
        {
            if (unitNumber == null) throw new ArgumentNullException(nameof(unitNumber));
            if (customerNumber == null) throw new ArgumentNullException(nameof(customerNumber));
            if (String.IsNullOrWhiteSpace(unitNumber)) throw new ArgumentException(nameof(unitNumber));
            if (String.IsNullOrWhiteSpace(customerNumber)) throw new ArgumentException(nameof(customerNumber));
            if (!tenancyStore.IsUnitNumberOccupied(unitNumber)) throw new InvalidOperationException("The unit is not occupied.");
            tenancyStore.Delete(unitNumber, customerNumber);
        }

        public decimal CalculateAmountDue(string customerNumber)
        {
            if (customerNumber == null) throw new ArgumentNullException(nameof(customerNumber));
            if (String.IsNullOrWhiteSpace(customerNumber)) throw new ArgumentException(nameof(customerNumber));
            var customerUnits = tenancyStore
                .GetCustomerUnits(customerNumber)
                .Select(u => new
                {
                    ReservationDate = u.ReservationDate,
                    AmountPaid = u.AmountPaid,
                    PricePerMonth = unitStore.GetPricePerMonth(u.UnitNumber)
                })
                .ToArray();
            var currentDateTime = dateService.GetCurrentDateTime();
            var costsPerUnit = customerUnits.Select(u => GetDifferenceInMonths(u.ReservationDate, currentDateTime) * u.PricePerMonth);
            var costsTotal = costsPerUnit.Sum();
            var paidTotal = customerUnits.Sum(u => u.AmountPaid);
            return costsTotal - paidTotal;
        }

        public void Pay(string customerNumber, decimal amount)
        {
            if (customerNumber == null) throw new ArgumentNullException(nameof(customerNumber));
            if (String.IsNullOrWhiteSpace(customerNumber)) throw new ArgumentException(nameof(customerNumber));
            if (amount < 0) throw new ArgumentException(nameof(amount));
            var currentDateTime = dateService.GetCurrentDateTime();
            var customerUnits = tenancyStore
                .GetCustomerUnits(customerNumber)
                .OrderBy(u => u.ReservationDate)
                .Select(u => new
                {
                    UnitNumber = u.UnitNumber,
                    TotalCost = GetDifferenceInMonths(u.ReservationDate, currentDateTime) * unitStore.GetPricePerMonth(u.UnitNumber),
                    AmountPaid = u.AmountPaid
                })
                .ToArray();
            var amountRemaining = amount;
            foreach (var unit in customerUnits)
            {
                var amountIsDue = unit.AmountPaid < unit.TotalCost;
                if (amountIsDue)
                {
                    var amountDue = unit.TotalCost - unit.AmountPaid;
                    var amountToApply = Math.Min(amountDue, amountRemaining);
                    tenancyStore.UpdateAmountPaid(unit.UnitNumber, amountToApply);
                    amountRemaining -= amountToApply;
                    var amountRemainingIsZero = amountRemaining == 0;
                    if (amountRemainingIsZero) break;
                }
            }
        }

        private int GetDifferenceInMonths(DateTime start, DateTime end) => ((end.Year * 12) + end.Month) - ((start.Year * 12) + start.Month);

        public IEnumerable<CustomerLookup> ListCustomersAndTenants() 
        {
            var customers = customerStore.ListCustomers();
            var tenants = tenancyStore.ListTenants();

            foreach (var customer in customers)
            {
                customer.UnitsReservedCount = tenants.Where(t=>t.CustomerNumber == customer.CustomerNumber).Count();
            }

            return customers;
        }

        public IEnumerable<TenantLookup> GetCustomerUnits(string customerNumber)
        {
            if (customerNumber == null) throw new ArgumentNullException(nameof(customerNumber));
            return tenancyStore.GetCustomerUnits(customerNumber);
        }

        public NewCustomer GetCustomerDetails(string customerNumber)
        {
            if (customerNumber == null) throw new ArgumentNullException(nameof(customerNumber));
            if (!customerStore.CustomerExists(customerNumber)) throw new InvalidOperationException("The customer number does not exist.");
            return customerStore.GetCustomer(customerNumber);
        }

        public void UpdateCustomerDetails(NewCustomer newCustomerDetails)
        {
            if (newCustomerDetails.CustomerNumber == null) throw new ArgumentException(nameof(newCustomerDetails.CustomerNumber));
            if (!customerStore.CustomerExists(newCustomerDetails.CustomerNumber)) throw new InvalidOperationException("The customer number does not exist.");
            customerStore.UpdateCustomer(newCustomerDetails);
        }
    }
}