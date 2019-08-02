﻿using System;

namespace HappyStorage.Core
{
    public class TenantLookup
    {
        public string UnitNumber { get; set; }
        public string CustomerNumber { get; set; }
        public DateTime ReservationDate { get; set; }
        public decimal AmountPaid { get; set; }
    }
}
