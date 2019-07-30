using System;
using System.Collections.Generic;

namespace HappyStorage.Core
{
    public interface ITenancyStore
    {
        void Create(string unitNumber, string customerNumber, DateTime reservationDate, decimal amountPaid);

        void Delete(string unitNumber, string customerNumber);

        IEnumerable<(string unitNumber, DateTime reservationDate, decimal amountPaid)> GetCustomerUnits(string customerNumber);

        IEnumerable<string> GetOccupiedUnitNumbers();

        bool UnitNumberOccupied(string unitNumber);

        void UpdateAmountPaid(string unitNumber, decimal amountToApply);
    }
}