using System;
using System.Collections.Generic;

namespace HappyStorage.Core
{
    public interface ITenancyStore
    {
        void Create(string unitNumber, string customerNumber, DateTime reservationDate, decimal amountPaid);

        void Delete(string unitNumber, string customerNumber);

        IEnumerable<TenantLookup> GetCustomerUnits(string customerNumber);

        IEnumerable<string> ListOccupiedUnits();

        IEnumerable<TenantLookup> ListTenants();

        bool IsUnitNumberOccupied(string unitNumber);

        void UpdateAmountPaid(string unitNumber, decimal amountToApply);
    }
}