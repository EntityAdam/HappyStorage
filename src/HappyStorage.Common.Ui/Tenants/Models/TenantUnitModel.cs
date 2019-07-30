using System;
using System.ComponentModel.DataAnnotations;

namespace HappyStorage.Common.Ui.Tenants.Models
{
    public class TenantUnitModel
    {
        [Display(Name = "Unit Number")]
        public string UnitNumber { get; set; }

        [Display(Name = "Reservation Date")]
        public DateTime ReservationDate { get; set; }

        [Display(Name = "Amount Paid")]
        public decimal AmountPaid { get; set; }
    }
}