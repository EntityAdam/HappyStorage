using System.ComponentModel.DataAnnotations;

namespace HappyStorage.Common.Ui.Units.Models
{
    public class UnitLookupModel
    {
        [Display(Name = "Unit Number")]
        public string UnitNumber { get; set; }

        [Display(Name = "Monthly Cost")]
        public decimal PricePerMonth { get; set; }
    }
}