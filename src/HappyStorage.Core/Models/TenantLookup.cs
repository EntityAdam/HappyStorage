using System;

namespace HappyStorage.Core.Models
{

    public record TenantLookup(string UnitNumber, string CustomerNumber, DateTime ReservationDate, decimal AmountPaid);
}
