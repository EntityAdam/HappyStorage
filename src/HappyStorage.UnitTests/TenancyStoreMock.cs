using HappyStorage.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HappyStorage.UnitTests
{
    internal class TenancyStoreMock : ITenancyStore
    {
        internal class Tenant
        {
            internal string UnitNumber { get; set; }
            internal string CustomerNumber { get; set; }
            internal DateTime ReservationDate { get; set; }
            internal decimal AmountPaid { get; set; }
        }

        internal readonly List<Tenant> Tenants = new List<Tenant>();

        public void Create(string unitNumber, string customerNumber, DateTime reservationDate, decimal amountPaid) => Tenants.Add(new Tenant()
        {
            UnitNumber = unitNumber,
            CustomerNumber = customerNumber,
            ReservationDate = reservationDate,
            AmountPaid = amountPaid
        });

        public IEnumerable<TenantLookup> ListTenants() => Tenants.Select(t => new TenantLookup { UnitNumber = t.UnitNumber, CustomerNumber = t.CustomerNumber, AmountPaid = t.AmountPaid, ReservationDate = t.ReservationDate });

        public void Delete(string unitNumber, string customerNumber) => Tenants.RemoveAll(t => t.UnitNumber == unitNumber && t.CustomerNumber == customerNumber);

        public IEnumerable<(string unitNumber, DateTime reservationDate, decimal amountPaid)> GetCustomerUnits(string customerNumber) => Tenants
            .Where(t => t.CustomerNumber == customerNumber)
            .Select(t => (t.UnitNumber, t.ReservationDate, t.AmountPaid));

        public IEnumerable<string> ListOccupiedUnits() => Tenants.Select(t => t.UnitNumber);

        public bool IsUnitNumberOccupied(string unitNumber) => Tenants.Any(t => t.UnitNumber == unitNumber);

        public void UpdateAmountPaid(string unitNumber, decimal amountToApply) => Tenants.Single(t => t.UnitNumber == unitNumber).AmountPaid += amountToApply;
    }
}