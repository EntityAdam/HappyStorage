using HappyStorage.Core;
using HappyStorage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HappyStorage.MemoryStorage
{
    public class MemoryTenancyStore : ITenancyStore
    {
        private class Tenant
        {
            internal string UnitNumber { get; set; }
            internal string CustomerNumber { get; set; }
            internal DateTime ReservationDate { get; set; }
            internal decimal AmountPaid { get; set; }
        }

        private readonly List<Tenant> Tenants = new List<Tenant>();

        public void Create(string unitNumber, string customerNumber, DateTime reservationDate, decimal amountPaid) => Tenants.Add(new Tenant()
        {
            UnitNumber = unitNumber,
            CustomerNumber = customerNumber,
            ReservationDate = reservationDate,
            AmountPaid = amountPaid
        });

        public IEnumerable<TenantLookup> ListTenants() => Tenants.Select(t => new TenantLookup { CustomerNumber = t.CustomerNumber, UnitNumber = t.UnitNumber, ReservationDate = t.ReservationDate, AmountPaid = t.AmountPaid});

        public void Delete(string unitNumber, string customerNumber) => Tenants.RemoveAll(t => t.UnitNumber == unitNumber && t.CustomerNumber == customerNumber);

        public IEnumerable<TenantLookup> GetCustomerUnits(string customerNumber) => Tenants
            .Where(t => t.CustomerNumber == customerNumber)
            .Select(t => new TenantLookup() { UnitNumber = t.UnitNumber, ReservationDate = t.ReservationDate, AmountPaid = t.AmountPaid });

        public IEnumerable<string> ListOccupiedUnits() => Tenants.Select(t => t.UnitNumber);

        public bool IsUnitNumberOccupied(string unitNumber) => Tenants.Any(t => t.UnitNumber == unitNumber);

        public void UpdateAmountPaid(string unitNumber, decimal amountToApply) => Tenants.Single(t => t.UnitNumber == unitNumber).AmountPaid += amountToApply;
    }
}