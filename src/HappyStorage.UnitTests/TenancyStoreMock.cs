using HappyStorage.Core;
using HappyStorage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HappyStorage.UnitTests
{
    internal class TenancyStoreMock : ITenancyStore
    {

        internal record Tenant(string UnitNumber, string CustomerNumber, DateTime ReservationDate, decimal AmountPaid, bool IsLocked = false, DateTime? LockedDateTime = null);

        internal readonly List<Tenant> Tenants = new();

        public void Create(string unitNumber, string customerNumber, DateTime reservationDate, decimal amountPaid) =>
            Tenants.Add(new Tenant(unitNumber, customerNumber, reservationDate, amountPaid));

        public IEnumerable<TenantLookup> ListTenants() => Tenants.Select(t => new TenantLookup(t.UnitNumber, t.CustomerNumber, t.ReservationDate, t.AmountPaid));

        public void Delete(string unitNumber, string customerNumber) => Tenants.RemoveAll(t => t.UnitNumber == unitNumber && t.CustomerNumber == customerNumber);

        public IEnumerable<TenantLookup> GetCustomerUnits(string customerNumber) => Tenants
            .Where(t => t.CustomerNumber == customerNumber)
            .Select(t => new TenantLookup(t.UnitNumber, t.CustomerNumber, t.ReservationDate, t.AmountPaid));

        public IEnumerable<string> ListOccupiedUnits() => Tenants.Select(t => t.UnitNumber);

        public bool IsUnitNumberOccupied(string unitNumber) => Tenants.Any(t => t.UnitNumber == unitNumber);

        public void UpdateAmountPaid(string unitNumber, decimal amountToApply)
        {
            var tenant = Tenants.Single(t => t.UnitNumber == unitNumber);
            var update = tenant with { AmountPaid = tenant.AmountPaid + amountToApply };
            Tenants.Remove(tenant);
            Tenants.Add(update);
        }

        public void Lock(string unitNumber, string customerNumber, DateTime dateTime)
        {
            var tenant = Tenants.Single(t => t.UnitNumber == unitNumber);
            var update = tenant with { IsLocked = true, LockedDateTime = dateTime };
            Tenants.Remove(tenant);
            Tenants.Add(update);
        }

        public void Unlock(string unitNumber)
        {
            var tenant = Tenants.Single(t => t.UnitNumber == unitNumber);
            var update = tenant with { IsLocked = false, LockedDateTime = null };
            Tenants.Remove(tenant);
            Tenants.Add(update);
        }

        public bool IsUnitLocked(string unitNumber) => Tenants.Single(t => t.UnitNumber == unitNumber).IsLocked;
    }
}