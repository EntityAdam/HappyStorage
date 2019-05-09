using System;
using System.Collections.Generic;
using System.Linq;
using HappyStorage.Core;

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

		public void Delete(string unitNumber, string customerNumber) => Tenants.RemoveAll(t => t.UnitNumber == unitNumber && t.CustomerNumber == customerNumber);

		public IEnumerable<(string unitNumber, DateTime reservationDate, decimal amountPaid)> GetCustomerUnits(string customerNumber) => Tenants
			.Where(t => t.CustomerNumber == customerNumber)
			.Select(t => (t.UnitNumber, t.ReservationDate, t.AmountPaid));

		public IEnumerable<string> GetOccupiedUnitNumbers() => Tenants.Select(t => t.UnitNumber);

		public bool UnitNumberOccupied(string unitNumber) => Tenants.Any(t => t.UnitNumber == unitNumber);

		public void UpdateAmountPaid(string unitNumber, decimal amountToApply) => Tenants.Single(t => t.UnitNumber == unitNumber).AmountPaid += amountToApply;
	}
}
