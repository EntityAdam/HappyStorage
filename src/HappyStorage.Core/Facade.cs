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

        public void CommissionNewUnit(NewUnit newUnit)
        {
            if (newUnit == null) throw new ArgumentNullException(nameof(newUnit));
            if (String.IsNullOrWhiteSpace(newUnit.UnitNumber)) throw new ArgumentException(nameof(newUnit.UnitNumber));
            if (unitStore.UnitExists(newUnit.UnitNumber)) throw new InvalidOperationException("The unit number already exists.");
            unitStore.Create(newUnit);
        }

        public void DecommissionUnit(string unitNumber)
        {
            if (unitNumber == null) throw new ArgumentNullException(nameof(unitNumber));
            if (String.IsNullOrWhiteSpace(unitNumber)) throw new ArgumentException(nameof(unitNumber));
            if (!unitStore.UnitExists(unitNumber)) throw new InvalidOperationException("The unit number does not exist.");
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
            customerStore.Delete(customerNumber);
        }

        public IEnumerable<AvailableUnit> FindAvailableUnits(bool? isClimateControlled, bool? isVehicleAccessible, int? minimumCubicFeet)
        {
            var units = unitStore.SearchUnits(isClimateControlled, isVehicleAccessible, minimumCubicFeet);
            var occupiedUnitNumbers = tenancyStore.GetOccupiedUnitNumbers().ToArray();
            return units.Where(u => !occupiedUnitNumbers.Contains(u.UnitNumber));
        }

        public void ReserveUnit(string unitNumber, string customerNumber)
        {
            if (unitNumber == null) throw new ArgumentNullException(nameof(unitNumber));
            if (customerNumber == null) throw new ArgumentNullException(nameof(customerNumber));
            if (String.IsNullOrWhiteSpace(unitNumber)) throw new ArgumentException(nameof(unitNumber));
            if (String.IsNullOrWhiteSpace(customerNumber)) throw new ArgumentException(nameof(customerNumber));
            if (tenancyStore.UnitNumberOccupied(unitNumber)) throw new InvalidOperationException("The unit is already occupied.");
            tenancyStore.Create(unitNumber, customerNumber, dateService.GetCurrentDateTime(), 0m);
        }

        public void ReleaseUnit(string unitNumber, string customerNumber)
        {
            if (unitNumber == null) throw new ArgumentNullException(nameof(unitNumber));
            if (customerNumber == null) throw new ArgumentNullException(nameof(customerNumber));
            if (String.IsNullOrWhiteSpace(unitNumber)) throw new ArgumentException(nameof(unitNumber));
            if (String.IsNullOrWhiteSpace(customerNumber)) throw new ArgumentException(nameof(customerNumber));
            if (!tenancyStore.UnitNumberOccupied(unitNumber)) throw new InvalidOperationException("The unit is not occupied.");
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
                    ReservationDate = u.reservationDate,
                    AmountPaid = u.amountPaid,
                    PricePerMonth = unitStore.GetPricePerMonth(u.unitNumber)
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
                .OrderBy(u => u.reservationDate)
                .Select(u => new
                {
                    UnitNumber = u.unitNumber,
                    TotalCost = GetDifferenceInMonths(u.reservationDate, currentDateTime) * unitStore.GetPricePerMonth(u.unitNumber),
                    AmountPaid = u.amountPaid
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

        public IEnumerable<CustomerLookup> ListCustomers() => customerStore.ListCustomers();

        //TODO: Add Test
        public IEnumerable<(string unitNumber, DateTime reservationDate, decimal amountPaid)> GetCustomerUnits(string customerNumber)
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
    }
}