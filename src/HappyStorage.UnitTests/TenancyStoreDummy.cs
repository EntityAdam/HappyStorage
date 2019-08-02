using HappyStorage.Core;
using System;
using System.Collections.Generic;

namespace HappyStorage.UnitTests
{
    internal class TenancyStoreDummy : ITenancyStore
    {
        public void Create(string unitNumber, string customerNumber, DateTime reservationDate, decimal amountPaid) => throw new NotSupportedException();

        public void Delete(string unitNumber, string customerNumber) => throw new NotSupportedException();

        public IEnumerable<TenantLookup> GetCustomerUnits(string customerNumber) => throw new NotSupportedException();

        public IEnumerable<string> ListOccupiedUnits() => throw new NotSupportedException();

        public IEnumerable<TenantLookup> ListTenants() => throw new NotSupportedException();

        public bool IsUnitNumberOccupied(string unitNumber) => throw new NotSupportedException();

        public void UpdateAmountPaid(string unitNumber, decimal amountToApply) => throw new NotSupportedException();
    }
}