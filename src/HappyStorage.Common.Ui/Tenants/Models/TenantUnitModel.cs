using System;
using System.ComponentModel.DataAnnotations;

namespace HappyStorage.Common.Ui.Tenants.Models
{
    public record TenantUnitModel(
        [Display(Name = "Unit Number")] string UnitNumber, 
        [Display(Name = "Reservation Date")] DateTime ReservationDate, 
        [Display(Name = "Amount Paid")] decimal AmountPaid,
        [Display(Name = "Unit Locked")] bool IsLocked,
        [Display(Name = "Date Locked")] DateTime? LockedDateTime
        );
}